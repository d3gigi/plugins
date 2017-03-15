
using System.Collections.Generic;
namespace Turbo.Plugins.Gigi
{
    public class ClassPower
    {
        #region Barbarian
        public class BarbarianPowers {
            // skills
            public const uint AncientSpear = 377453 ;
            public const uint Avalanche =  353447 ;
            public const uint Bash = 79242 ;
            public const uint BattleRage = 79076 ;
            public const uint CallOfTheAncients = 80049 ;
            public const uint Cleave = 80263 ;
            public const uint Earthquake = 98878 ;
            public const uint Frenzy = 78548 ;
            public const uint FuriousCharge = 97435 ;
            public const uint GroundStomp = 79446 ;
            public const uint HammerOfTheAncients = 80028 ;
            public const uint IgnorePain = 79528 ;
            public const uint Leap = 93409 ;
            public const uint Overpower = 159169 ;
            public const uint OverpowerCowKing = 368239 ;
            public const uint Rend = 70472 ;
            public const uint Revenge = 109342 ;
            public const uint SeismicSlam = 86989 ;
            public const uint Sprint = 78551 ;
            public const uint ThreateningShout = 79077 ;
            public const uint WarCry = 375483 ;
            public const uint WeaponThrow = 377452 ;
            public const uint Whirlwind = 96296 ;
            public const uint WrathOfTheBerserker = 79607 ;
            // passives
			public const uint Animosity =  205228 ;
            public const uint BerserkerRage = 205187 ;
            public const uint Bloodthirst = 205217 ;
            public const uint BoonOfBulKathos = 204603 ;
            public const uint Brawler = 205133 ;
            public const uint EarthenMight = 361661 ;
            public const uint InspiringPresence = 205546 ;
            public const uint Juggernaut = 205707 ;
            public const uint NervesOfSteel = 217819 ;
            public const uint NoEscape = 204725 ;
            public const uint PoundOfFlesh = 205205 ;
            public const uint Rampage = 296572 ;
            public const uint Relentless = 205398 ;
            public const uint Ruthless = 205175 ;
            public const uint Superstition = 205491 ;
            public const uint SwordAndBoard = 340877 ;
            public const uint ToughAsNails = 205848 ;
            public const uint Unforgiving = 205300 ;
            public const uint WeaponsMaster = 206147 ;

            public static List<uint> GetAllPowers(){
                List<uint> res = new List<uint>{
                    // skills
                    AncientSpear,
                    Avalanche,
                    Bash,
                    BattleRage,
                    CallOfTheAncients,
                    Cleave,
                    Earthquake,
                    Frenzy,
                    FuriousCharge,
                    GroundStomp,
                    HammerOfTheAncients,
                    IgnorePain,
                    Leap,
                    Overpower,
                    OverpowerCowKing,
                    Rend,
                    Revenge,
                    SeismicSlam,
                    Sprint,
                    ThreateningShout,
                    WarCry,
                    WeaponThrow,
                    Whirlwind,
                    WrathOfTheBerserker,
                    // passives
                    Animosity,
                    BerserkerRage,
                    Bloodthirst,
                    BoonOfBulKathos,
                    Brawler,
                    EarthenMight,
                    InspiringPresence,
                    Juggernaut,
                    NervesOfSteel,
                    NoEscape,
                    PoundOfFlesh,
                    Rampage,
                    Relentless,
                    Ruthless,
                    Superstition,
                    SwordAndBoard,
                    ToughAsNails,
                    Unforgiving,
                    WeaponsMaster
                };
                return res;
            }
        }
        #endregion Barbarian

        #region Crusader
        public class CrusaderPowers {
            // skills
            public const uint AkaratSChampion = 269032 ;
            public const uint BlessedHammer = 266766 ;
            public const uint BlessedShield = 266951 ;
            public const uint Bombardment = 284876 ;
            public const uint Condemn = 266627 ;
            public const uint Consecration = 273941 ;
            public const uint CrushingResolve = 267818 ;
            public const uint FallingSword = 239137 ;
            public const uint FistOfTheHeavens = 239218 ;
            public const uint HeavenSFury = 316014 ;
            public const uint IronSkin = 291804 ;
            public const uint Judgment = 267600 ;
            public const uint Justice = 325216 ;
            public const uint LawsOfFate = 290960 ;
            public const uint LawsOfHope = 290912 ;
            public const uint LawsOfHopeSecondary = 342279 ;
            public const uint LawsOfJustice = 266722 ;
            public const uint LawsOfJusticeSecondary = 342280 ;
            public const uint LawsOfValor = 290946 ;
            public const uint LawsOfValorSecondary = 342281 ;
            public const uint Phalanx = 330729 ;
            public const uint Provoke = 290545 ;
            public const uint Punish = 285903 ;
            public const uint ShieldBash = 353492 ;
            public const uint ShieldGlare = 268530 ;
            public const uint Slash = 289243 ;
            public const uint Smite = 286510 ;
            public const uint SteedCharge = 243853 ;
            public const uint SweepAttack = 239042 ;

            // passives
            public const uint Blunt = 348773 ;
            public const uint DivineFortress = 356176 ;
            public const uint Fanaticism = 357269 ;
            public const uint Fervor = 357218 ;
            public const uint Finery = 311629 ;
            public const uint HeavenlyStrength = 286177 ;
            public const uint HoldYourGround = 302500 ;
            public const uint HolyCause = 310804 ;
            public const uint Indestructible = 309830 ;
            public const uint Insurmountable = 310640 ;
            public const uint IronMaiden = 310783 ;
            public const uint LongArmOfTheLaw = 310678 ;
            public const uint LordCommander = 348741 ;
            public const uint Renewal = 356173 ;
            public const uint Righteousness = 356147 ;
            public const uint ToweringShield = 356052 ;
            public const uint Vigilant = 310626 ;
            public const uint Wrathful = 310775 ;

            public static List<uint> GetAllPowers(){
                List<uint> res = new List<uint>{
                    // skills
                    AkaratSChampion,
                    BlessedHammer,
                    BlessedShield,
                    Bombardment,
                    Condemn,
                    Consecration,
                    CrushingResolve,
                    FallingSword,
                    FistOfTheHeavens,
                    HeavenSFury,
                    IronSkin,
                    Judgment,
                    Justice,
                    LawsOfFate,
                    LawsOfHope,
                    LawsOfHopeSecondary,
                    LawsOfJustice,
                    LawsOfJusticeSecondary,
                    LawsOfValor,
                    LawsOfValorSecondary,
                    Phalanx,
                    Provoke,
                    Punish,
                    ShieldBash,
                    ShieldGlare,
                    Slash,
                    Smite,
                    SteedCharge,
                    SweepAttack,
                    // passives
                    Blunt,
                    DivineFortress,
                    Fanaticism,
                    Fervor,
                    Finery,
                    HeavenlyStrength,
                    HoldYourGround,
                    HolyCause,
                    Indestructible,
                    Insurmountable,
                    IronMaiden,
                    LongArmOfTheLaw,
                    LordCommander,
                    Renewal,
                    Righteousness,
                    ToweringShield,
                    Vigilant,
                    Wrathful,
                };
                return res;
            }
        }
        #endregion Crusader

        #region DemonHunter
        public class DemonHunterPowers {
            // skills
            public const uint Bolas = 77552 ;
            public const uint Caltrops = 129216 ;
            public const uint Chakram = 129213 ;
            public const uint ClusterArrow =  129214 ;
            public const uint Companion = 365311 ;
            public const uint CompanionPassive = 365312 ;
            public const uint ElementalArrow = 131325 ;
            public const uint EntanglingShot = 361936 ;
            public const uint EvasiveFire = 377450 ;
            public const uint FanOfKnives = 77546 ;
            public const uint Grenade = 86610 ;
            public const uint HungeringArrow = 129215 ;
            public const uint Impale = 131366 ;
            public const uint MarkedForDeath = 130738 ;
            public const uint Multishot = 77649 ;
            public const uint Preparation = 129212 ;
            public const uint PreparationPassive = 324845 ;
            public const uint RainOfVengeance = 130831 ;
            public const uint RapidFire = 131192 ;
            public const uint Sentry = 129217 ;
            public const uint ShadowPower = 130830 ;
            public const uint SmokeScreen = 130695 ;
            public const uint SpikeTrap = 75301 ;
            public const uint Strafe = 134030 ;
            public const uint Vault = 111215 ;
            public const uint Vengeance = 302846 ;
            // passives
            public const uint Ambush = 352920 ;
            public const uint Archery = 209734 ;
            public const uint Awareness = 324770 ;
            public const uint Ballistics = 155723 ;
            public const uint BloodVengeance = 155714 ;
            public const uint Brooding = 210801 ;
            public const uint CullTheWeak = 155721 ;
            public const uint CustomEngineering = 208610 ;
            public const uint Grenadier = 208779 ;
            public const uint HotPursuit = 155725 ;
            public const uint Leech = 439525 ;
            public const uint NightStalker = 218350 ;
            public const uint NumbingTraps = 218398 ;
            public const uint Perfectionist = 155722 ;
            public const uint Sharpshooter = 155715 ;
            public const uint SingleOut = 338859 ;
            public const uint SteadyAim = 164363 ;
            public const uint TacticalAdvantage = 218385 ;
            public const uint ThrillOfTheHunt = 211225 ;

            public static List<uint> GetAllPowers(){
                List<uint> res  = new List<uint>{
                        // skills
                        Bolas,
                        Caltrops,
                        Chakram,
                        ClusterArrow,
                        Companion,
                        CompanionPassive,
                        ElementalArrow,
                        EntanglingShot,
                        EvasiveFire,
                        FanOfKnives,
                        Grenade,
                        HungeringArrow,
                        Impale,
                        MarkedForDeath,
                        Multishot,
                        Preparation,
                        PreparationPassive,
                        RainOfVengeance,
                        RapidFire,
                        Sentry,
                        ShadowPower,
                        SmokeScreen,
                        SpikeTrap,
                        Strafe,
                        Vault,
                        Vengeance,
                        // passives
                        Ambush,
                        Archery,
                        Awareness,
                        Ballistics,
                        BloodVengeance,
                        Brooding,
                        CullTheWeak,
                        CustomEngineering,
                        Grenadier,
                        HotPursuit,
                        Leech,
                        NightStalker,
                        NumbingTraps,
                        Perfectionist,
                        Sharpshooter,
                        SingleOut,
                        SteadyAim,
                        TacticalAdvantage,
                        ThrillOfTheHunt
                };
                return res;
            }
        }
        #endregion DemonHunter

        #region Monk
        public class MonkPowers {
            // skills
            public const uint BlindingFlash = 136954 ;
            public const uint BreathOfHeaven = 69130 ;
            public const uint CripplingWave = 96311 ;
            public const uint CycloneStrike = 223473 ;
            public const uint DashingStrike = 312736 ;
            public const uint DeadlyReach = 96019 ;
            public const uint Epiphany = 312307 ;
            public const uint ExplodingPalm = 97328 ;
            public const uint FistsOfThunder = 95940 ;
            public const uint InnerSanctuary = 317076 ;
            public const uint LashingTailKick = 111676 ;
            public const uint MantraOfConviction = 375089 ; //375088 ;
            public const uint MantraOfHealing = 373154 ; //373143 ;
            public const uint MantraOfRetribution = 375083 ; //375082 ;
            public const uint MantraOfSalvation = 375050 ; //375049 ;  
            public const uint MysticAlly = 362102 ;
            public const uint Serenity = 96215 ;
            public const uint SevenSidedStrike = 96694 ;
            public const uint SweepingWind = 96090 ;
            public const uint TempestRush = 121442 ;
            public const uint WaveOfLight =  96033 ;
            public const uint WayOfTheHundredFists = 97110 ;
            // passives
            public const uint Alacrity = 156492 ;
            public const uint BeaconOfYtar = 209104 ;
            public const uint ChantOfResonance = 156467 ;
            public const uint CombinationStrike = 218415 ;
            public const uint Determination = 402633 ;
            public const uint ExaltedSoul = 209027 ;
            public const uint FleetFooted = 209029 ;
            public const uint Harmony = 404168 ;
            public const uint Momentum = 341559 ;
            public const uint MythicRhythm = 315271 ;
            public const uint NearDeathExperience = 156484 ;
            public const uint RelentlessAssault = 404245 ;
            public const uint Resolve = 211581 ;
            public const uint SeizeTheInitiative = 209628 ;
            public const uint SixthSense = 209622 ;
            public const uint TheGuardianSPath = 209812 ;
            public const uint Transcendence = 209250 ;
            public const uint Unity = 368899 ;

            public static List<uint> GetAllPowers(){
                List<uint> res = new List<uint>{
                    // skills
                    BlindingFlash,
                    BreathOfHeaven,
                    CripplingWave,
                    CycloneStrike,
                    DashingStrike,
                    DeadlyReach,
                    Epiphany,
                    ExplodingPalm,
                    FistsOfThunder,
                    InnerSanctuary,
                    LashingTailKick,
                    MantraOfConviction,
                    MantraOfHealing,
                    MantraOfRetribution,
                    MantraOfSalvation,
                    MysticAlly,
                    Serenity,
                    SevenSidedStrike,
                    SweepingWind,
                    TempestRush,
                    WaveOfLight,
                    WayOfTheHundredFists,
                    // passives
                    Alacrity,
                    BeaconOfYtar,
                    ChantOfResonance,
                    CombinationStrike,
                    Determination,
                    ExaltedSoul,
                    FleetFooted,
                    Harmony,
                    Momentum,
                    MythicRhythm,
                    NearDeathExperience,
                    RelentlessAssault,
                    Resolve,
                    SeizeTheInitiative,
                    SixthSense,
                    TheGuardianSPath,
                    Transcendence,
                    Unity,
                };
                return res;
            }

        }
        #endregion Monk

        #region Witchdoctor
        public class WitchdoctorPowers {
            // skills
            public const uint AcidCloud = 70455 ;
            public const uint BigBadVoodoo = 117402 ;
            public const uint CorpseSpiders = 69866 ;
            public const uint FetishArmy = 72785 ;
            public const uint Firebats = 105963 ;
            public const uint Firebomb = 67567 ;
            public const uint Gargantuan = 30624 ;
            public const uint GraspOfTheDead = 69182 ;
            public const uint Haunt = 83602 ;
            public const uint Hex = 30631 ;
            public const uint Horrify = 67668 ;
            public const uint LocustSwarm = 69867 ;
            public const uint MassConfusion = 67600 ;
            public const uint Piranhas = 347265 ;
            public const uint PlagueOfToads = 106465 ;
            public const uint PoisonDart = 103181 ;
            public const uint Sacrifice = 102572 ;
            public const uint SoulHarvest = 67616 ;
            public const uint SpiritBarrage = 108506 ;
            public const uint SpiritWalk = 106237 ;
            public const uint SummonZombieDogs = 102573 ;
            public const uint VeSummonZombieDogs = 109560 ;
            public const uint WallOfDeath = 134837 ;
            public const uint ZombieCharger = 74003 ;
            // passives
            public const uint BadMedicine = 217826 ;
            public const uint BloodRitual = 208568 ;
            public const uint CircleOfLife = 208571 ;
            public const uint ConfidenceRitual = 442741 ;
            public const uint CreepingDeath = 340908 ;
            public const uint FetishSycophants = 218588 ;
            public const uint FierceLoyalty = 208639 ;
            public const uint GraveInjustice = 218191 ;
            public const uint GruesomeFeast = 208594 ;
            public const uint JungleFortitude = 217968 ;
            public const uint MidnightFeast = 340909 ;
            public const uint PierceTheVeil = 208628 ;
            public const uint RushOfEssence = 208565 ;
            public const uint SpiritualAttunement = 208569 ;
            public const uint SpiritVessel = 218501 ;
            public const uint SwamplandAttunement = 340910 ;
            public const uint TribalRites = 208601 ;
            public const uint VisionQuest = 209041 ;
            public const uint ZombieHandler = 208563 ;

            public static List<uint> GetAllPowers(){
                List<uint> res = new List<uint>{
                    //skils
                    AcidCloud,
                    BigBadVoodoo,
                    CorpseSpiders,
                    FetishArmy,
                    Firebats,
                    Firebomb,
                    Gargantuan,
                    GraspOfTheDead,
                    Haunt,
                    Hex,
                    Horrify,
                    LocustSwarm,
                    MassConfusion,
                    Piranhas,
                    PlagueOfToads,
                    PoisonDart,
                    Sacrifice,
                    SoulHarvest,
                    SpiritBarrage,
                    SpiritWalk,
                    SummonZombieDogs,
                    VeSummonZombieDogs,
                    WallOfDeath,
                    ZombieCharger,
                    // passives
                    BadMedicine,
                    BloodRitual,
                    CircleOfLife,
                    ConfidenceRitual,
                    CreepingDeath,
                    FetishSycophants,
                    FierceLoyalty,
                    GraveInjustice,
                    GruesomeFeast,
                    JungleFortitude,
                    MidnightFeast,
                    PierceTheVeil,
                    RushOfEssence,
                    SpiritualAttunement,
                    SpiritVessel,
                    SwamplandAttunement,
                    TribalRites,
                    VisionQuest,
                    ZombieHandler
                };
                return res;
            }
        }
        #endregion Witchdoctor

        #region Wizard
        public class WizardPowers {
            // skills
            public const uint ArcaneOrb = 30668 ;
            public const uint ArcaneTorrent = 134456 ;
            public const uint Archon = 134872 ;
            public const uint ArchonDisintegrationWaveArcane = 135238 ;
            public const uint ArchonDisintegrationWaveFire = 392890 ;
            public const uint ArchonDisintegrationWaveLightning = 392891 ;
            public const uint ArchonDisintegrationWaveCold = 392889 ;
            public const uint ArchonSlowTime = 135663;
            public const uint BlackHole = 243141 ;
            public const uint Blizzard = 30680 ;
            public const uint DiamondSkin = 75599 ;
            public const uint Disintegrate = 91549 ;
            public const uint Electrocute = 1765 ;
            public const uint EnergyArmor = 86991 ;
            public const uint EnergyTwister = 77113 ;
            public const uint ExplosiveBlast = 87525 ;
            public const uint Familiar = 99120 ;
            public const uint FrostNova = 30718 ;
            public const uint Hydra = 30725 ;
            public const uint IceArmor = 73223 ;
            public const uint MagicMissile = 30744 ;
            public const uint MagicWeapon = 76108 ;
            public const uint Meteor = 69190 ;
            public const uint MirrorImage = 98027 ;
            public const uint RayOfFrost = 93395 ;
            public const uint ShockPulse = 30783 ;
            public const uint SlowTime = 1769 ;
            public const uint SpectralBlade = 71548 ;
            public const uint StormArmor = 74499 ;
            public const uint Teleport = 168344 ;
            public const uint WaveOfForce = 30796 ;
            // passives
            public const uint ArcaneDynamo = 208823 ;
            public const uint AstralPresence = 208472 ;
            public const uint Audacity = 341540 ;
            public const uint Blur = 208468 ;
            public const uint ColdBlooded = 226301 ;
            public const uint Conflagration = 218044 ;
            public const uint Dominance = 341344 ;
            public const uint ElementalExposure = 342326 ; 
            public const uint Evocation = 208473 ;
            public const uint GalvanizingWard = 208541 ;
            public const uint GlassCannon = 208471 ;
            public const uint Illusionist = 208547 ;
            public const uint Paralysis = 226348 ;
            public const uint PowerHungry = 208478 ;
            public const uint Prodigy = 208493 ;            
            public const uint TemporalFlux = 208477 ;
            public const uint UnstableAnomaly = 208474 ;
            public const uint UnwaveringWill = 298038 ;
                
            public static List<uint> GetAllPowers(){
                List<uint> res = new List<uint>{
                    // skills
                    ArcaneOrb,
                    ArcaneTorrent,
                    Archon,
                    ArchonDisintegrationWaveArcane,
                    ArchonDisintegrationWaveFire,
                    ArchonDisintegrationWaveLightning,
                    ArchonDisintegrationWaveCold,
                    ArchonSlowTime,
                    BlackHole,
                    Blizzard,
                    DiamondSkin,
                    Disintegrate,
                    Electrocute,
                    EnergyArmor,
                    EnergyTwister,
                    ExplosiveBlast,
                    Familiar,
                    FrostNova,
                    Hydra,
                    IceArmor,
                    MagicMissile,
                    MagicWeapon,
                    Meteor,
                    MirrorImage,
                    RayOfFrost,
                    ShockPulse,
                    SlowTime,
                    SpectralBlade,
                    StormArmor,
                    Teleport,
                    WaveOfForce,
                    // passives
                    ArcaneDynamo,
                    AstralPresence,
                    Audacity,
                    Blur,
                    ColdBlooded,
                    Conflagration,
                    Dominance,
                    ElementalExposure,
                    Evocation,
                    GalvanizingWard,
                    GlassCannon,
                    Illusionist,
                    Paralysis,
                    PowerHungry,
                    Prodigy,
                    TemporalFlux,
                    UnstableAnomaly,
                    UnwaveringWill
                };
                return res;
            }
        }
        #endregion Wizard

        public static List<uint> GetAllClassPowers(){
            List<uint> res = BarbarianPowers.GetAllPowers();
            res.AddRange(CrusaderPowers.GetAllPowers());
            res.AddRange(DemonHunterPowers.GetAllPowers());
            res.AddRange(MonkPowers.GetAllPowers());
            res.AddRange(WitchdoctorPowers.GetAllPowers());
            res.AddRange(WizardPowers.GetAllPowers());
            return res;
        }
    }

    public class GemPower
    {
        public const uint BaneOfThePowerfulPrimary = 383014;
        public const uint BaneOfThePowerfulSecondary = 451157;
        public const uint BaneOfTheTrappedPrimary = 403456;
        public const uint BaneOfTheTrappedSecondary = 403457;
        public const uint BaneOfTheStrickenPrimary = 428348;
        public const uint BaneOfTheStrickenSecondary = 428349;
        public const uint BoonOfTheHoarderPrimary = 403470;
        public const uint BoonOfTheHoarderSecondary = 403784;
        public const uint BoyarskysChipPrimary = 428352;
        public const uint BoyarskysChipSecondary = 428353;
        public const uint EnforcerPrimary = 403466;
        public const uint EnforcerSecondary = 403472;
        public const uint EsotericAlterationPrimary = 428029;
        public const uint EsotericAlterationSecondary = 428030;
        public const uint GemOfEasePrimary = 403459;
        public const uint GemOfTheToxinPrimary = 403461;
        public const uint GemOfTheToxinSecondary = 403556;
        public const uint GogokOfSwiftnessPrimary = 403464;
        public const uint GogokOfSwiftnessSecondary = 403524;
        public const uint IceblinkPimary = 428354;
        public const uint IceblinSecondary = 428356;
        public const uint InvigoratingGemstonePrimary = 403465;
        public const uint InvigoratingGemstoneSecondary = 403624;
        public const uint MirinaeTeardropOfStarweaverPrimary = 403463;
        public const uint MirinaeTeardropOfStarweaverSecondary = 403620;
        public const uint MoltenWidebeestsGizzardPrimary = 428031;
        public const uint MoltenWidebeestsGizzardSecondary = 428032;
        public const uint MoratoriumPrimary = 403467;
        public const uint MoratoriumSecondary = 403687;
        public const uint MutilationGuardPrimary = 428350;
        public const uint MutilationGuardSecondary = 428351;
        public const uint PainEnhancerPrimary = 403462;
        public const uint PainEnhancerSecondary = 403600;
        public const uint RedSoulShardPrimary = 454736;
        public const uint RedSoulShardSecondary = 454737;
        public const uint SimplicitysStrengthPrimary = 403469;
        public const uint SimplicitysStrengthSecondary  = 403473;
        public const uint TaegukPrimary = 403471;
        public const uint TaegukSecondary = 403785;
        public const uint WreathOfLightningPrimary = 403460;
        public const uint WreathOfLightningSecondary = 403560;
        public const uint ZeisStoneOfVengeancePrimary = 403468;
        public const uint ZeisStoneOfVengeanceSecondary = 403727;

        public static List<uint> GetAllGemPowers(){
            List<uint> res = new List<uint>{
                BaneOfThePowerfulPrimary,
                BaneOfThePowerfulSecondary,
                BaneOfTheTrappedPrimary,
                BaneOfTheTrappedSecondary,
                BaneOfTheStrickenPrimary,
                BaneOfTheStrickenSecondary,
                BoonOfTheHoarderPrimary,
                BoonOfTheHoarderSecondary,
                BoyarskysChipPrimary,
                BoyarskysChipSecondary,
                EnforcerPrimary,
                EnforcerSecondary,
                EsotericAlterationPrimary,
                EsotericAlterationSecondary,
                GemOfEasePrimary,
                GemOfTheToxinPrimary,
                GemOfTheToxinSecondary,
                GogokOfSwiftnessPrimary,
                GogokOfSwiftnessSecondary,
                IceblinkPimary,
                IceblinSecondary,
                InvigoratingGemstonePrimary,
                InvigoratingGemstoneSecondary,
                MirinaeTeardropOfStarweaverPrimary,
                MirinaeTeardropOfStarweaverSecondary,
                MoltenWidebeestsGizzardPrimary,
                MoltenWidebeestsGizzardSecondary,
                MoratoriumPrimary,
                MoratoriumSecondary,
                MutilationGuardPrimary,
                MutilationGuardSecondary,
                PainEnhancerPrimary,
                PainEnhancerSecondary,
                RedSoulShardPrimary,
                RedSoulShardSecondary,
                SimplicitysStrengthPrimary,
                SimplicitysStrengthSecondary ,
                TaegukPrimary,
                TaegukSecondary,
                WreathOfLightningPrimary,
                WreathOfLightningSecondary,
                ZeisStoneOfVengeancePrimary,
                ZeisStoneOfVengeanceSecondary
            };
            return res;
        }
    }

    public class ItemPower
    {
        public const uint AetherWalker = 397788 ;
        public const uint AhavarionSpearOfLycander = 318868 ;
        public const uint AkkhansAddendum = 445943 ;
        public const uint AkkhansLeniency = 446063 ;
        public const uint AkkhansManacles = 446008 ;
        public const uint AncestorsGrace = 318378 ;
        public const uint AncientParthanDefenders = 318770 ;
        public const uint AnessaziEdge = 318720 ;
        public const uint AquilaCuirass = 449064 ;
        public const uint ArchmagesVicalyke = 318777 ;
        public const uint Arcstone = 359598 ;
        public const uint ArmorOfTheKindRegent = 318892 ;
        public const uint ArreatsLaw = 441349 ;
        public const uint ArthefsSparkOfLife = 318757 ;
        public const uint AshnagarrsBloodBracer = 449043 ;
        public const uint BakuliJungleWraps = 451163 ;
        public const uint Balance = 449021 ;
        public const uint BalefulRemnant = 359545 ;
        public const uint BandOfHollowWhispers = 364345 ;
        public const uint BandOfMight = 447060 ;
        public const uint BandOfTheRueChambers = 318434 ;
        public const uint BastionsRevered = 397792 ;
        public const uint BeckonSail = 318420 ;
        public const uint BeltOfTheTrove = 423235 ;
        public const uint BeltOfTranscendence = 430671 ;
        public const uint BindingOfTheLost = 440598 ;
        public const uint BindingsOfTheLesserGods = 449222 ;
        public const uint Blackfeather = 318882 ;
        public const uint BladeOfProphecy = 359540 ;
        public const uint BladeOfTheTribes = 444969 ;
        public const uint BladeOfTheWarlord = 447375 ;
        public const uint BlessedOfHaull = 430681 ;
        public const uint BloodBrother = 402456 ;
        public const uint BovineBardiche = 318382 ;
        public const uint BracerOfFury = 446162 ;
        public const uint BracersOfDestruction = 441305 ;
        public const uint BracersOfTheFirstMen = 441279 ;
        public const uint BrokenCrown = 423231 ;
        public const uint BrokenPromises = 402462 ;
        public const uint BulKathossWeddingBand = 364340 ;
        public const uint ButchersCarver = 246118 ;
        public const uint CamsRebuttal = 318358 ;
        public const uint CapeOfTheDarkNight = 318421 ;
        public const uint Carnevil = 318758 ;
        public const uint CesarsMemento = 449031 ;
        public const uint Chaingmail = 318798 ;
        public const uint ChainOfShadows = 445266 ;
        public const uint ChanonBolter = 318784 ;
        public const uint ChilaniksChain = 318821 ;
        public const uint Cindercoat = 318790 ;
        public const uint CoilsOfTheFirstSpider = 440790 ;
        public const uint ConventionOfElements = 430674 ;
        public const uint CordOfTheSherma = 434008 ;
        public const uint CorruptedAshbringer = 402455 ;
        public const uint CountessJuliasCameo = 318381 ;
        public const uint CrashingRain = 359554 ;
        public const uint CrownOfThePrimus = 423239 ;
        public const uint CrystalFist = 451170 ;
        public const uint CusterianWristguards = 359557 ;
        public const uint Darklight = 451313 ;
        public const uint DarkMagesShade = 318788 ;
        public const uint DeadlyRebirth = 318808 ;
        public const uint DeathseersCowl = 318857 ;
        public const uint DeathWatchMantle = 434005 ;
        public const uint Deathwish = 449063 ;
        public const uint DepthDiggers = 402416 ;
        public const uint DishonoredLegacy = 441294 ;
        public const uint DovuEnergyTrap = 318867 ;
        public const uint DrakonsLesson = 430678 ;
        public const uint DreadIron = 430679 ;
        public const uint ElusiveRing = 446187 ;
        public const uint EnchantingFavor = 318835 ;
        public const uint EternalUnion = 402413 ;
        public const uint Eunjangdo = 402457 ;
        public const uint EyeOfPeshkov = 318431 ;
        public const uint FaithfulMemory = 454927 ;
        public const uint FateOfTheFell = 359552 ;
        public const uint FazulasImprobableChain = 437854 ;
        public const uint FireWalkers = 434010 ;
        public const uint FlailOfTheAscended = 451164 ;
        public const uint Fleshrake = 451168 ;
        public const uint FlyingDragon = 246562 ;
        public const uint FortressBallista = 447816 ;
        public const uint FragmentOfDestiny = 450472 ;
        public const uint Frostburn = 451167 ;
        public const uint Fulminator = 441681 ;
        public const uint FuryOfTheAncients = 441280 ;
        public const uint GabrielsVambraces = 436521 ;
        public const uint Genzaniku = 364311 ;
        public const uint GestureOfOrpheus = 318376 ;
        public const uint GirdleOfGiants = 451237 ;
        public const uint GladiatorGauntlets = 318799 ;
        public const uint GoldenFlense = 359546 ;
        public const uint Goldwrap = 318875 ;
        public const uint GungdoGear = 423242 ;
        public const uint GyanaNaKashu = 318426 ;
        public const uint GyrfalconsFoote = 318850 ;
        public const uint Hack = 318869 ;
        public const uint HaloOfArlyse = 429648 ;
        public const uint HaloOfKarini = 451158 ;
        public const uint HammerJammers = 446502 ;
        public const uint HandOfTheProphet = 318377 ;
        public const uint HarringtonWaistguard = 318881 ;
        public const uint HauntingGirdle = 434966 ;
        public const uint HauntOfVaxo = 318782 ;
        public const uint HeartOfIron = 446615 ;
        public const uint HellcatWaistguard = 454934 ;
        public const uint HergbrashsBinding = 449048 ;
        public const uint HexingPantsOfMrYan = 318817 ;
        public const uint HillenbrandsTrainingSword = 359604 ;
        public const uint HomingPads = 318801 ;
        public const uint HuntersWrath = 440743 ;
        public const uint HwojWrap = 318800 ;
        public const uint IncenseTorchOfTheGrandTemple = 318776 ;
        public const uint Ingeom = 402458 ;
        public const uint InviolableFaith = 318894 ;
        public const uint IrontoeMudsputters = 318877 ;
        public const uint JacesHammerOfVigilance = 266766 ;
        public const uint JangsEnvelopment = 318795 ;
        public const uint Jawbreaker = 318432 ;
        public const uint JeramsBracers = 441278 ;
        public const uint JohannasArgument = 436430 ;
        public const uint JustiniansMercy = 318895 ;
        public const uint KarleisPoint = 445279 ;
        public const uint KassarsRetribution = 359538 ;
        public const uint KekegisUnbreakableSpirit = 318751 ;
        public const uint KhassettsCordOfRighteousness = 451238 ;
        public const uint KmarTenclip = 318423 ;
        public const uint KredesFlame = 318865 ;
        public const uint KrelmsBuffBelt = 359602 ;
        public const uint KrelmsBuffBracers = 359591 ;
        public const uint Kridershot = 318379 ;
        public const uint KyoshirosBlade = 447368 ;
        public const uint KyoshirosSoul = 447130 ;
        public const uint LakumbasOrnament = 445692 ;
        public const uint Lamentation = 359593 ;
        public const uint LastBreath = 447030 ;
        public const uint LefebvresSoliloquy = 449236 ;
        public const uint LeonineBowOfHashir = 397784 ;
        public const uint LiannasWings = 447696 ;
        public const uint LionsClaw = 402451 ;
        public const uint LordGreenstonesFan = 445274 ;
        public const uint LutSocks = 318810 ;
        public const uint MadawcsSorrow = 318744 ;
        public const uint Madstone = 402540 ;
        public const uint Magefist = 451166 ;
        public const uint MalothsFocus = 246780 ;
        public const uint ManaldHeal = 454930 ;
        public const uint MantleOfChanneling = 446640 ;
        public const uint MarasKaleidoscope = 318719 ;
        public const uint MaskOfJeram = 318411 ;
        public const uint MoonlightWard = 364343 ;
        public const uint MordullusPromise = 447029 ;
        public const uint NemesisBracers = 318820 ;
        public const uint NilfursBoast = 451186 ;
        public const uint Oathkeeper = 447372 ;
        public const uint OculusRing = 402461 ;
        public const uint OdynSon = 364325 ;
        public const uint OdysseysEnd = 428220 ;
        public const uint Omnislash = 430682 ;
        public const uint OmrynsChain = 423229 ;
        public const uint PintosPride = 447295 ;
        public const uint PoxFaulds = 434009 ;
        public const uint PrideOfCassius = 318419 ;
        public const uint PromiseOfGlory = 318871 ;
        public const uint PuzzleRing = 318375 ;
        public const uint Quetzalcoatl = 318796 ;
        public const uint RabidStrike = 454929 ;
        public const uint RakoffsGlassOfLife = 318410 ;
        public const uint RanslorsFolly = 423240 ;
        public const uint RazorStrop = 318241 ;
        public const uint RechelsRingOfLarceny = 318870 ;
        public const uint RelicOfAkarat = 318377 ;
        public const uint Remorseless = 397802 ;
        public const uint RhenhoFlayer = 318812 ;
        public const uint RibaldEtchings = 318377 ;
        public const uint Rimeheart = 318864 ;
        public const uint RingOfEmptiness = 445694 ;
        public const uint RiveraDancers = 447043 ;
        public const uint RogarsHugeStone = 318861 ;
        public const uint SacredHarness = 440434 ;
        public const uint SacredHarvester = 410217 ;
        public const uint SaffronWrap = 454918 ;
        public const uint SashOfKnives = 434038 ;
        public const uint Scarbringer = 451240 ;
        public const uint Scourge = 364321 ;
        public const uint Scrimshaw = 442477 ;
        public const uint SeborsNightmare = 434039 ;
        public const uint SerpentsSparker = 318371 ;
        public const uint ShardOfHate = 359587 ;
        public const uint ShiMizusHaori = 318779 ;
        public const uint SkeletonKey = 318835 ;
        public const uint SkularsSalvation = 444929 ;
        public const uint SkullGrasp = 451160 ;
        public const uint SkullOfResonance = 318773 ;
        public const uint SkySplitter = 433993 ;
        public const uint Skywarden = 359550 ;
        public const uint SlipkasLetterOpener = 359604 ;
        public const uint SloraksMadness = 91549 ;
        public const uint SmokingThurible = 318835 ;
        public const uint Solanium = 318873 ;
        public const uint SpauldersOfZakara = 318858 ;
        public const uint SpiritGuards = 430289 ;
        public const uint StaffOfChiroptera = 446734 ;
        public const uint StalgardsDecimator = 318412 ;
        public const uint Standoff = 446592 ;
        public const uint StArchewsGage = 434007 ;
        public const uint Starfire = 451242 ;
        public const uint StarmetalKukri = 318724 ;
        public const uint StormCrow = 364338 ;
        public const uint StringOfEars = 446541 ;
        public const uint StrongarmBracers = 318772 ;
        public const uint SuWongDiviner = 442478 ;
        public const uint SwampLandWaders = 451161 ;
        public const uint Swiftmount = 359537 ;
        public const uint SwordOfIllWill = 446641 ;
        public const uint TalismanOfAranoch = 318715 ;
        public const uint TaskerandTheo = 318731 ;
        public const uint TheBarber = 454932 ;
        public const uint TheBurningAxeOfSankis = 246113 ;
        public const uint TheButchersSickle = 248484 ;
        public const uint TheCloakOfTheGarwulf = 318300 ;
        public const uint TheCrudestBoots = 409811 ;
        public const uint TheDaggerOfDarts = 402447 ;
        public const uint TheDemonsDemise = 451243 ;
        public const uint TheEssOfJohan = 318759 ;
        public const uint TheFistOfAzTurrasq = 318433 ;
        public const uint TheFlowOfEternity = 451162 ;
        public const uint TheFurnace = 318753 ;
        public const uint TheGavelOfJudgment = 435040 ;
        public const uint TheGidbinn = 364316 ;
        public const uint TheGrandVizier = 402448 ;
        public const uint TheGrinReaper = 251572 ;
        public const uint TheLawsOfSeph = 318428 ;
        public const uint TheMagistrate = 318786 ;
        public const uint TheMindsEye = 318824 ;
        public const uint TheMortalDrama = 359553 ;
        public const uint ThePaddle = 247777 ;
        public const uint TheShameOfDelsere = 445427 ;
        public const uint TheShortMansFinger = 430677 ;
        public const uint TheSmolderingCore = 318791 ;
        public const uint TheSpiderQueensGrasp = 318732 ;
        public const uint TheStarOfAzkaranth = 318716 ;
        public const uint TheSwami = 440336 ;
        public const uint TheTallMansFinger = 318806 ;
        public const uint TheThreeHundredthSpear = 446638 ;
        public const uint TheTormentor = 247572 ;
        public const uint TheTwistedSword = 446195 ;
        public const uint TheUndisputedChampion = 423233 ;
        public const uint ThunderfuryBlessedBladeOfTheWindseeker = 318763 ;
        public const uint TiklandianVisage = 318774 ;
        public const uint TragOulCoils = 451239 ;
        public const uint TzoKrinsGaze = 318811 ;
        public const uint UnstableScepter = 445920 ;
        public const uint VadimsSurge = 359604 ;
        public const uint ValtheksRebuke = 318792 ;
        public const uint VambracesOfSescheron = 447839 ;
        public const uint VelvetCamaral = 318740 ;
        public const uint VengefulWind = 402411 ;
        public const uint Vigilance = 367008 ;
        public const uint VileWard = 397783 ;
        public const uint VisageOfGiyua = 318385 ;
        public const uint VisageOfGunes = 446655 ;
        public const uint VoosJuicer = 446969 ;
        public const uint WandOfWoh = 318789 ;
        public const uint WarhelmOfKassar = 449049 ;
        public const uint WarstaffOfGeneralQuang = 318430 ;
        public const uint WarzechianArmguards = 318771 ;
        public const uint Wizardspike = 364312 ;
        public const uint WojahnniAssaulter = 451165 ;
        public const uint WrapsOfClarity = 441517 ;
        public const uint Wyrdward = 434036 ;
        public const uint XephirianAmulet = 318718 ;
        public const uint ZoeysSecret = 446639 ;

        public static List<uint> GetAllPowers(){
            List<uint> res = new List<uint>{
                AetherWalker,
                AhavarionSpearOfLycander,
                AkkhansAddendum,
                AkkhansLeniency,
                AkkhansManacles,
                AncestorsGrace,
                AncientParthanDefenders,
                AnessaziEdge,
                AquilaCuirass,
                ArchmagesVicalyke,
                Arcstone,
                ArmorOfTheKindRegent,
                ArreatsLaw,
                ArthefsSparkOfLife,
                AshnagarrsBloodBracer,
                BakuliJungleWraps,
                Balance,
                BalefulRemnant,
                BandOfHollowWhispers,
                BandOfMight,
                BandOfTheRueChambers,
                BastionsRevered,
                BeckonSail,
                BeltOfTheTrove,
                BeltOfTranscendence,
                BindingOfTheLost,
                BindingsOfTheLesserGods,
                Blackfeather,
                BladeOfProphecy,
                BladeOfTheTribes,
                BladeOfTheWarlord,
                BlessedOfHaull,
                BloodBrother,
                BovineBardiche,
                BracerOfFury,
                BracersOfDestruction,
                BracersOfTheFirstMen,
                BrokenCrown,
                BrokenPromises,
                BulKathossWeddingBand,
                ButchersCarver,
                CamsRebuttal,
                CapeOfTheDarkNight,
                Carnevil,
                CesarsMemento,
                Chaingmail,
                ChainOfShadows,
                ChanonBolter,
                ChilaniksChain,
                Cindercoat,
                CoilsOfTheFirstSpider,
                ConventionOfElements,
                CordOfTheSherma,
                CorruptedAshbringer,
                CountessJuliasCameo,
                CrashingRain,
                CrownOfThePrimus,
                CrystalFist,
                CusterianWristguards,
                Darklight,
                DarkMagesShade,
                DeadlyRebirth,
                DeathseersCowl,
                DeathWatchMantle,
                Deathwish,
                DepthDiggers,
                DishonoredLegacy,
                DovuEnergyTrap,
                DrakonsLesson,
                DreadIron,
                ElusiveRing,
                EnchantingFavor,
                EternalUnion,
                Eunjangdo,
                EyeOfPeshkov,
                FaithfulMemory,
                FateOfTheFell,
                FazulasImprobableChain,
                FireWalkers,
                FlailOfTheAscended,
                Fleshrake,
                FlyingDragon,
                FortressBallista,
                FragmentOfDestiny,
                Frostburn,
                Fulminator,
                FuryOfTheAncients,
                GabrielsVambraces,
                Genzaniku,
                GestureOfOrpheus,
                GirdleOfGiants,
                GladiatorGauntlets,
                GoldenFlense,
                Goldwrap,
                GungdoGear,
                GyanaNaKashu,
                GyrfalconsFoote,
                Hack,
                HaloOfArlyse,
                HaloOfKarini,
                HammerJammers,
                HandOfTheProphet,
                HarringtonWaistguard,
                HauntingGirdle,
                HauntOfVaxo,
                HeartOfIron,
                HellcatWaistguard,
                HergbrashsBinding,
                HexingPantsOfMrYan,
                HillenbrandsTrainingSword,
                HomingPads,
                HuntersWrath,
                HwojWrap,
                IncenseTorchOfTheGrandTemple,
                Ingeom,
                InviolableFaith,
                IrontoeMudsputters,
                JacesHammerOfVigilance,
                JangsEnvelopment,
                Jawbreaker,
                JeramsBracers,
                JohannasArgument,
                JustiniansMercy,
                KarleisPoint,
                KassarsRetribution,
                KekegisUnbreakableSpirit,
                KhassettsCordOfRighteousness,
                KmarTenclip,
                KredesFlame,
                KrelmsBuffBelt,
                KrelmsBuffBracers,
                Kridershot,
                KyoshirosBlade,
                KyoshirosSoul,
                LakumbasOrnament,
                Lamentation,
                LastBreath,
                LefebvresSoliloquy,
                LeonineBowOfHashir,
                LiannasWings,
                LionsClaw,
                LordGreenstonesFan,
                LutSocks,
                MadawcsSorrow,
                Madstone,
                Magefist,
                MalothsFocus,
                ManaldHeal,
                MantleOfChanneling,
                MarasKaleidoscope,
                MaskOfJeram,
                MoonlightWard,
                MordullusPromise,
                NemesisBracers,
                NilfursBoast,
                Oathkeeper,
                OculusRing,
                OdynSon,
                OdysseysEnd,
                Omnislash,
                OmrynsChain,
                PintosPride,
                PoxFaulds,
                PrideOfCassius,
                PromiseOfGlory,
                PuzzleRing,
                Quetzalcoatl,
                RabidStrike,
                RakoffsGlassOfLife,
                RanslorsFolly,
                RazorStrop,
                RechelsRingOfLarceny,
                RelicOfAkarat,
                Remorseless,
                RhenhoFlayer,
                RibaldEtchings,
                Rimeheart,
                RingOfEmptiness,
                RiveraDancers,
                RogarsHugeStone,
                SacredHarness,
                SacredHarvester,
                SaffronWrap,
                SashOfKnives,
                Scarbringer,
                Scourge,
                Scrimshaw,
                SeborsNightmare,
                SerpentsSparker,
                ShardOfHate,
                ShiMizusHaori,
                SkeletonKey,
                SkularsSalvation,
                SkullGrasp,
                SkullOfResonance,
                SkySplitter,
                Skywarden,
                SlipkasLetterOpener,
                SloraksMadness,
                SmokingThurible,
                Solanium,
                SpauldersOfZakara,
                SpiritGuards,
                StaffOfChiroptera,
                StalgardsDecimator,
                Standoff,
                StArchewsGage,
                Starfire,
                StarmetalKukri,
                StormCrow,
                StringOfEars,
                StrongarmBracers,
                SuWongDiviner,
                SwampLandWaders,
                Swiftmount,
                SwordOfIllWill,
                TalismanOfAranoch,
                TaskerandTheo,
                TheBarber,
                TheBurningAxeOfSankis,
                TheButchersSickle,
                TheCloakOfTheGarwulf,
                TheCrudestBoots,
                TheDaggerOfDarts,
                TheDemonsDemise,
                TheEssOfJohan,
                TheFistOfAzTurrasq,
                TheFlowOfEternity,
                TheFurnace,
                TheGavelOfJudgment,
                TheGidbinn,
                TheGrandVizier,
                TheGrinReaper,
                TheLawsOfSeph,
                TheMagistrate,
                TheMindsEye,
                TheMortalDrama,
                ThePaddle,
                TheShameOfDelsere,
                TheShortMansFinger,
                TheSmolderingCore,
                TheSpiderQueensGrasp,
                TheStarOfAzkaranth,
                TheSwami,
                TheTallMansFinger,
                TheThreeHundredthSpear,
                TheTormentor,
                TheTwistedSword,
                TheUndisputedChampion,
                ThunderfuryBlessedBladeOfTheWindseeker,
                TiklandianVisage,
                TragOulCoils,
                TzoKrinsGaze,
                UnstableScepter,
                VadimsSurge,
                ValtheksRebuke,
                VambracesOfSescheron,
                VelvetCamaral,
                VengefulWind,
                Vigilance,
                VileWard,
                VisageOfGiyua,
                VisageOfGunes,
                VoosJuicer,
                WandOfWoh,
                WarhelmOfKassar,
                WarstaffOfGeneralQuang,
                WarzechianArmguards,
                Wizardspike,
                WojahnniAssaulter,
                WrapsOfClarity,
                Wyrdward,
                XephirianAmulet,
                ZoeysSecret
            };
            return res;
        }
    }
}
