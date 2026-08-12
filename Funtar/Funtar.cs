using JetBrains.Annotations;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Tables;

namespace Funtar;

[Injectable(TypePriority = OnLoadOrder.Preload + 1), UsedImplicitly]
public class Funtar(TemplateTable templateTable, TradersTable tradersTable, GlobalTable globalTable) : IOnLoad
{
    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        var itemsDb = templateTable.Items;
        
        if (!itemsDb.TryGetValue(ItemTpl.ARMOR_MFUNTAR_BODY, out var armor) 
            || !tradersTable.TryGetValue(Traders.PEACEKEEPER, out var peacekeeper)) throw new NullReferenceException();

        PushToTemplateItemSlots(armor);
        PushToGlobals(globalTable);
        PushToTrader(peacekeeper);
        
        return Task.CompletedTask;
    }

    private static void PushToTemplateItemSlots(TemplateItem tpl)
    {
        List<Slot> newSlots =
        [
            new()
            {
                Id = "6944c0e1dc404a744f0b1930",
                Name = "Front_plate",
                Parent = ItemTpl.ARMOR_MFUNTAR_BODY.ToString(),
                MergeSlotWithChildren = true,
                Properties = new SlotProperties
                {
                    Filters =
                    [
                        new SlotFilter
                        {
                            Filter =
                            [
                                ItemTpl.ARMORPLATE_AR500_LEGACY_PLATE_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_CULT_LOCUST_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_CULT_TERMITE_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GAC_3S15M_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GAC_4SSS2_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GLOBAL_ARMORS_STEEL_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_KITECO_SCIV_SA_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_KIBA_ARMS_TITAN_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_KIBA_ARMS_STEEL_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_MONOCLETE_LEVEL_III_PE_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_NESCO_4400SAMC_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_NEWSPHERETECH_LEVEL_III_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_SPRTN_ELAPHROS_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_SPRTN_OMEGA_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_TALLCOM_GUARDIAN_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_SAPI_LEVEL_III_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_ESAPI_LEVEL_IV_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GRANIT_BR4_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GRANIT_BR5_BALLISTIC_PLATE
                            ],
                            Plate = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                            ArmorColliders = [],
                            ArmorPlateColliders = ["Plate_Granit_SAPI_chest"],
                            BluntDamageReduceFromSoftArmor = true,
                            Locked = false
                        }
                    ]
                },
                Prototype = "64479fdf9731c8fadc0642c1",
                Required = false
            },
            new()
            {
                Id = "6944c0e1dc404a744f0b1931",
                Name = "Back_plate",
                Parent = ItemTpl.ARMOR_MFUNTAR_BODY.ToString(),
                MergeSlotWithChildren = true,
                Properties = new SlotProperties
                {
                    Filters =
                    [
                        new SlotFilter
                        {
                            Filter =
                            [
                                ItemTpl.ARMORPLATE_AR500_LEGACY_PLATE_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_CULT_LOCUST_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_CULT_TERMITE_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GAC_3S15M_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GAC_4SSS2_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GLOBAL_ARMORS_STEEL_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_KITECO_SCIV_SA_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_KIBA_ARMS_TITAN_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_KIBA_ARMS_STEEL_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_MONOCLETE_LEVEL_III_PE_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_NESCO_4400SAMC_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_NEWSPHERETECH_LEVEL_III_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_SPRTN_ELAPHROS_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_SPRTN_OMEGA_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_TALLCOM_GUARDIAN_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_SAPI_LEVEL_III_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_ESAPI_LEVEL_IV_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GRANIT_BR4_BALLISTIC_PLATE,
                                ItemTpl.ARMORPLATE_GRANIT_BR5_BALLISTIC_PLATE
                            ],
                            Plate = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                            ArmorColliders = [],
                            ArmorPlateColliders = ["Plate_Granit_SAPI_back"],
                            BluntDamageReduceFromSoftArmor = true,
                            Locked = false
                        }
                    ]
                },
                Prototype = "64479fdf9731c8fadc0642c1",
                Required = false
            },
            new()
            {
                Id = "6944c0e1dc404a744f0b1932",
                Name = "Left_side_plate",
                Parent = ItemTpl.ARMOR_MFUNTAR_BODY.ToString(),
                MergeSlotWithChildren = true,
                Properties = new SlotProperties
                {
                    Filters =
                    [
                        new SlotFilter
                        {
                            Filter =
                            [
                                ItemTpl.ARMORPLATE_SSAPI_LEVEL_III_BALLISTIC_PLATE_SIDE,
                                ItemTpl.ARMORPLATE_ESBI_LEVEL_IV_BALLISTIC_PLATE_SIDE,
                                ItemTpl.ARMORPLATE_GRANIT_BALLISTIC_PLATE_SIDE
                            ],
                            Plate = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                            ArmorColliders = [],
                            ArmorPlateColliders = ["Plate_Granit_SSAPI_side_left_high"],
                            BluntDamageReduceFromSoftArmor = true,
                            Locked = false
                        }
                    ]
                },
                Prototype = "64479fdf9731c8fadc0642c1",
                Required = false
            },
            new()
            {
                Id = "6944c0e1dc404a744f0b1933",
                Name = "Right_side_plate",
                Parent = ItemTpl.ARMOR_MFUNTAR_BODY.ToString(),
                MergeSlotWithChildren = true,
                Properties = new SlotProperties
                {
                    Filters =
                    [
                        new SlotFilter
                        {
                            Filter =
                            [
                                ItemTpl.ARMORPLATE_SSAPI_LEVEL_III_BALLISTIC_PLATE_SIDE,
                                ItemTpl.ARMORPLATE_ESBI_LEVEL_IV_BALLISTIC_PLATE_SIDE,
                                ItemTpl.ARMORPLATE_GRANIT_BALLISTIC_PLATE_SIDE
                            ],
                            Plate = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                            ArmorColliders = [],
                            ArmorPlateColliders = ["Plate_Granit_SSAPI_side_right_high"],
                            BluntDamageReduceFromSoftArmor = true,
                            Locked = false
                        }
                    ]
                },
                Prototype = "64479fdf9731c8fadc0642c1",
                Required = false
            }
        ];
        var slots = tpl.Properties?.Slots?.ToList();
        if (slots == null) throw new NullReferenceException();
        newSlots.AddRange(slots);
        tpl.Properties!.Slots = newSlots;
    }

    private static void PushToGlobals(GlobalTable globals)
    {
        var itemPresets = globals.ItemPresets;
        var mfUntarArmor = itemPresets[new MongoId("657121c5f1074598bf0c02c8")];
        var baseItemId = mfUntarArmor.Items[0].Id.ToString();
        List<Item> newItems =
        [
            new()
            {
                Id = new MongoId("6944c3d6dc404a744f0b1934"),
                Template = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                ParentId = baseItemId,
                SlotId = "Front_plate"
            },
            new()
            {
                Id = new MongoId("6944c3d6dc404a744f0b1935"),
                Template = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                ParentId = baseItemId,
                SlotId = "Back_plate"
            }
        ];
        
        mfUntarArmor.Items.AddRange(newItems);
    }

    private static void PushToTrader(Trader trader)
    {
        var assort = trader.Assort;
        List<Item> newItems =
        [
            new()
            {
                Id = new MongoId("6944c9e5dc404a744f0b1936"),
                Template = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                ParentId = "686e344e6c2a18ed6b0ea434",
                SlotId = "Front_plate"
            },
            new()
            {
                Id = new MongoId("6944c9e5dc404a744f0b1937"),
                Template = ItemTpl.ARMORPLATE_PRTCTR_LIGHTWEIGHT_BALLISTIC_PLATE,
                ParentId = "686e344e6c2a18ed6b0ea434",
                SlotId = "Back_plate"
            }
        ];
        assort.Items.AddRange(newItems);
    }
}