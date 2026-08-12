using System.Reflection;
using System.Text.Json;
using Funtar.Config;
using JetBrains.Annotations;
using SPTarkov.Common.Models.Logging;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers.Server;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Tables;

namespace Funtar.Compat;

[Injectable(TypePriority = OnLoadOrder.Preload + 80090), UsedImplicitly]
public class UNTARGoHome(BotTable botTable, ISptLogger<UNTARGoHome> logger, ModHelper modHelper) : IOnLoad
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public Task OnLoadAsync(CancellationToken cancellationToken)

    {
        var botTypes = botTable.Types;
        
        if (!botTypes.TryGetValue("bossuntarlead", out var bossUNTARLead) ||
            !botTypes.TryGetValue("followeruntar", out var followerUNTAR) ||
            !botTypes.TryGetValue("followeruntarmarksman", out var followerUNTARMarksman) ||
            !botTypes.TryGetValue("bossuntarofficer", out var bossUNTAROfficer)) 
            return Task.CompletedTask;

        var path = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        if (!File.Exists($"{path}/untar_compat.json"))
        {
            var json = JsonSerializer.Serialize(new UNTARConfig(), _jsonSerializerOptions);
            File.WriteAllText($"{path}/untar_compat.json", json);
        }
        var config = modHelper.GetJsonDataFromFile<UNTARConfig>(path, "untar_compat.json");

        if (config.AllBotsHaveMaxArmour)
        {
            if (!SetArmorPlates(bossUNTARLead, ArmorType.BossUltra, true) ||
                !SetArmorPlates(followerUNTAR, ArmorType.BossUltra, true) ||            
                !SetArmorPlates(followerUNTARMarksman,  ArmorType.BossUltra, true) ||
                !SetArmorPlates(bossUNTAROfficer, ArmorType.BossUltra, true))
                return Task.CompletedTask;
        }
        else
        {
            if (!SetArmorPlates(bossUNTARLead, ArmorType.BossUltra, true) ||
                !SetArmorPlates(followerUNTAR, ArmorType.Medium, false) ||            
                !SetArmorPlates(followerUNTARMarksman,  ArmorType.Low, false) ||
                !SetArmorPlates(bossUNTAROfficer, ArmorType.BossHigh, true))
                return Task.CompletedTask;
        }
        
        logger.Success("[FUNTAR] Successfully loaded UNTAR Go Home compatibility.");
        
        if (config.AllBotsHaveMaxArmour)
        {
            logger.Success("[FUNTAR] All UNTAR operatives are now equipped with level VI full-torso armour. Good luck.");
        }
        
        return Task.CompletedTask;
    }

    private static bool SetArmorPlates(BotType? botType, ArmorType armorType, bool sides)
    {
        if (botType is null) return false;

        var mods = botType.BotInventory.Mods;
        var untarVest = mods[ItemTpl.ARMOR_MFUNTAR_BODY];

        Dictionary<string, HashSet<MongoId>> armorPlates;
        
        var mainPlate = armorType switch
        {
            ArmorType.Low => ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
            ArmorType.Medium => ItemTpl.ARMORPLATE_MONOCLETE_LEVEL_III_PE_BALLISTIC_PLATE,
            ArmorType.BossHigh => ItemTpl.ARMORPLATE_SAPI_LEVEL_III_BALLISTIC_PLATE,
            ArmorType.BossUltra => ItemTpl.ARMORPLATE_ESAPI_LEVEL_IV_BALLISTIC_PLATE,
            _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
        };

        if (sides)
        {
            var sidePlate = armorType switch
            {
                ArmorType.Low => null!,
                ArmorType.Medium => null!,
                ArmorType.BossHigh => ItemTpl.ARMORPLATE_SSAPI_LEVEL_III_BALLISTIC_PLATE_SIDE,
                ArmorType.BossUltra => ItemTpl.ARMORPLATE_ESBI_LEVEL_IV_BALLISTIC_PLATE_SIDE,
                _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
            };
            
            armorPlates = new Dictionary<string, HashSet<MongoId>>
            {
                ["Front_plate"] = [mainPlate],
                ["Back_plate"] = [mainPlate],
                ["Left_side_plate"] = [sidePlate],
                ["Right_side_plate"] = [sidePlate],
            };
        }
        else
        {
            armorPlates = new Dictionary<string, HashSet<MongoId>>
            {
                ["Front_plate"] = [mainPlate],
                ["Back_plate"] = [mainPlate],
                ["Left_side_plate"] = [],
                ["Right_side_plate"] = [],
            };
        }
        
        foreach (var plate in untarVest)
        {
            armorPlates.Add(plate.Key, plate.Value);
        }

        mods[ItemTpl.ARMOR_MFUNTAR_BODY] = armorPlates;
        return true;
    }
    
    private enum ArmorType
    {
        Low,
        Medium,
        BossHigh,
        BossUltra
    }
}