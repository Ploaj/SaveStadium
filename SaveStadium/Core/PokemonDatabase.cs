using System;
using System.Collections.Generic;

namespace SaveStadium.Core
{
    public enum GrowthRate
    {
        Slow,
        Medium_Slow,
        Medium_Fast,
        Fast
    }

    public class PokemonInfo
    {
        public int BaseHP { get; internal set; }
        public int BaseATK { get; internal set; }
        public int BaseDEF { get; internal set; }
        public int BaseSPCATK { get; internal set; }
        public int BaseSPCDEF { get; internal set; }
        public int BaseSPD { get; internal set; }
        public string Name { get; internal set; }
        public int ID { get; internal set; }
        public GrowthRate GrowthRate { get; internal set; }

        public PokemonInfo(int baseHP, int baseATK, int baseDEF, int baseSPCATK, int baseSPCDEF, int baseSPD, string name, int iD, string growthRate)
        {
            BaseHP = baseHP;
            BaseATK = baseATK;
            BaseDEF = baseDEF;
            BaseSPCATK = baseSPCATK;
            BaseSPCDEF = baseSPCDEF;
            BaseSPD = baseSPD;
            Name = name;
            ID = iD;
            GrowthRate = (GrowthRate)Enum.Parse(typeof(GrowthRate), growthRate);
        }

        public int CalcuateHP(int level, int iv, int ev)
        {
            return (int)(Math.Floor((((BaseHP + iv) * 2f + ev) * level) / 100f) + level + 10);
        }

        private int CalStat(int baseValue, int level, int iv, int ev)
        {
            return (int)(Math.Floor((((baseValue + iv) * 2f + ev) * level) / 100f) + 5);
        }

        public int CalcuateATK(int level, int iv, int ev)
        {
            return CalStat(BaseATK, level, iv, ev);
        }

        public int CalcuateDEF(int level, int iv, int ev)
        {
            return CalStat(BaseDEF, level, iv, ev);
        }

        public int CalcuateSPD(int level, int iv, int ev)
        {
            return CalStat(BaseSPD, level, iv, ev);
        }

        public int CalcuateSPCATK(int level, int iv, int ev)
        {
            return CalStat(BaseSPCATK, level, iv, ev);
        }

        public int CalcuateSPCDEF(int level, int iv, int ev)
        {
            return CalStat(BaseSPCDEF, level, iv, ev);
        }

        public bool IsFemale(int ATKIV)
        {
            if (PokemonDatabase.FemaleOnlyPokemon.Contains(ID))
                return true;
            if (PokemonDatabase.MaleOnlyPokemon.Contains(ID))
                return false;
            if (PokemonDatabase.Male7to1.Contains(ID))
                return ATKIV <= 1;
            if (PokemonDatabase.Male3to1.Contains(ID))
                return ATKIV <= 3;
            if (PokemonDatabase.Male1to3.Contains(ID))
                return ATKIV <= 11;

            return ATKIV <= 7;
        }
        public bool HasGender()
        {
            return !PokemonDatabase.GenderlessPokemon.Contains(ID);
        }

        public int GetEXPForLevel(int level)
        {
            switch (GrowthRate)
            {
                case GrowthRate.Slow:
                    return 5 * (level * level * level) / 4;
                case GrowthRate.Medium_Slow:
                    return (int)((6f / 5f * Math.Pow(level, 3)) - 15 * Math.Pow(level, 2) + 100 * level - 140);
                case GrowthRate.Medium_Fast:
                    return (int)Math.Pow(level, 3);
                case GrowthRate.Fast:
                    return (int)((4 * Math.Pow(level, 3)) / 5);
            }
            return 0;
        }
    }

    public class AttackInfo
    {
        public string Name { get; internal set; }
        public string Type { get; internal set; }
        public int PP { get; internal set; }
        public int Power { get; internal set; }
        public string Accuracy { get; internal set; }

        public AttackInfo(string name, string type, int pP, int power, string accuracy)
        {
            Name = name;
            Type = type;
            PP = pP;
            Power = power;
            Accuracy = accuracy;
        }
    }

    public class PokemonDatabase
    {
        public static readonly HashSet<int> MaleOnlyPokemon = new HashSet<int>()
        {
            32,33,34,106,107,128,236,237
        };
        public static readonly HashSet<int> FemaleOnlyPokemon = new HashSet<int>()
        {
            29,30,31,113,115,124,238,241,242
        };
        public static readonly HashSet<int> GenderlessPokemon = new HashSet<int>()
        {
            81,82,100,101,120,121,132,137,144,145,146,150,151,201,233,243,244,245,249,250,251
        };
        public static readonly HashSet<int> Male7to1 = new HashSet<int>()
        {
            1,2,3,4,5,6,7,8,9,133,134,135,136,138,139,140,141,142,143,152,154,155,156,157,158,159,160,175,176,196,197
        };
        public static readonly HashSet<int> Male3to1 = new HashSet<int>()
        {
            58,59,63,64,65,66,67,68,125,126,239,240
        };
        public static readonly HashSet<int> Male1to3 = new HashSet<int>()
        {
            35,36,37,38,39,40,209,210,222,173,174
        };

        public static readonly List<AttackInfo> AttackInfo = new List<Core.AttackInfo>()
        {
            new AttackInfo("---","Normal",0,0,"0%"),
            new AttackInfo("---","Normal",0,0,"0%"),
            new AttackInfo("Karate Chop","Fighting",25,50,"100%"),
new AttackInfo("Double Slap","Normal",10,15,"85%"),
new AttackInfo("Comet Punch","Normal",15,18,"85%"),
new AttackInfo("Mega Punch","Normal",20,80,"85%"),
new AttackInfo("Pay Day","Normal",20,40,"100%"),
new AttackInfo("Fire Punch","Fire",15,75,"100%"),
new AttackInfo("Ice Punch","Ice",15,75,"100%"),
new AttackInfo("Thunder Punch","Electric",15,75,"100%"),
new AttackInfo("Scratch","Normal",35,40,"100%"),
new AttackInfo("Vice Grip","Normal",30,55,"100%"),
new AttackInfo("Guillotine","Normal",5,-1,"Accuracy is ((level of user - level of target) + 30). Cannot hit Pokemon of a higher level."),
new AttackInfo("Razor Wind","Normal",10,80,"75%"),
new AttackInfo("Swords Dance","Normal",30,-1,"-1"),
new AttackInfo("Cut","Normal",30,50,"95%"),
new AttackInfo("Gust","Flying",35,40,"100%"),
new AttackInfo("Wing Attack","Flying",35,35,"100%"),
new AttackInfo("Whirlwind","Normal",20,-1,"100%"),
new AttackInfo("Fly","Flying",15,70,"95%"),
new AttackInfo("Bind","Normal",20,15,"75%"),
new AttackInfo("Slam","Normal",20,80,"75%"),
new AttackInfo("Vine Whip","Grass",10,35,"100%"),
new AttackInfo("Stomp","Normal",20,65,"100%"),
new AttackInfo("Double Kick","Fighting",30,30,"100%"),
new AttackInfo("Mega Kick","Normal",5,120,"75%"),
new AttackInfo("Jump Kick","Fighting",25,70,"95%"),
new AttackInfo("Rolling Kick","Fighting",15,60,"85%"),
new AttackInfo("Sand Attack","Ground",15,-1,"100%"),
new AttackInfo("Headbutt","Normal",15,70,"100%"),
new AttackInfo("Horn Attack","Normal",25,65,"100%"),
new AttackInfo("Fury Attack","Normal",20,15,"85%"),
new AttackInfo("Horn Drill","Normal",5,-1,"Accuracy is ((level of user - level of target) + 30). Cannot hit Pokemon of a higher level."),
new AttackInfo("Tackle","Normal",35,35,"95%"),
new AttackInfo("Body Slam","Normal",15,85,"100%"),
new AttackInfo("Wrap","Normal",20,15,"85%"),
new AttackInfo("Take Down","Normal",20,90,"85%"),
new AttackInfo("Thrash","Normal",20,90,"100%"),
new AttackInfo("Double-Edge","Normal",15,120,"100%"),
new AttackInfo("Tail Whip","Normal",30,-1,"100%"),
new AttackInfo("Poison Sting","Poison",35,15,"100%"),
new AttackInfo("Twineedle","Bug",20,25,"100%"),
new AttackInfo("Pin Missile","Bug",20,14,"85%"),
new AttackInfo("Leer","Normal",30,-1,"100%"),
new AttackInfo("Bite","Dark",25,60,"100%"),
new AttackInfo("Growl","Normal",40,-1,"100%"),
new AttackInfo("Roar","Normal",20,-1,"100%"),
new AttackInfo("Sing","Normal",15,-1,"55%"),
new AttackInfo("Supersonic","Normal",20,-1,"55%"),
new AttackInfo("Sonic Boom","Normal",20,-20,"90%"), //Always deals 20 HP damage
new AttackInfo("Disable","Normal",20,-1,"55%"),
new AttackInfo("Acid","Poison",30,40,"100%"),
new AttackInfo("Ember","Fire",25,40,"100%"),
new AttackInfo("Flamethrower","Fire",15,95,"100%"),
new AttackInfo("Mist","Ice",30,-1,"-1"),
new AttackInfo("Water Gun","Water",25,40,"100%"),
new AttackInfo("Hydro Pump","Water",5,120,"80%"),
new AttackInfo("Surf","Water",15,95,"100%"),
new AttackInfo("Ice Beam","Ice",10,95,"100%"),
new AttackInfo("Blizzard","Ice",5,120,"70%"),
new AttackInfo("Psybeam","Psychic",20,65,"100%"),
new AttackInfo("Bubble Beam","Water",20,65,"100%"),
new AttackInfo("Aurora Beam","Ice",20,65,"100%"),
new AttackInfo("Hyper Beam","Normal",5,150,"90%"),
new AttackInfo("Peck","Flying",35,35,"100%"),
new AttackInfo("Drill Peck","Flying",20,80,"100%"),
new AttackInfo("Submission","Fighting",25,80,"80%"),
new AttackInfo("Low Kick","Fighting",20,50,"90%"),
new AttackInfo("Counter","Fighting",20,-1,"100%"),
new AttackInfo("Seismic Toss","Fighting",20,-1,"100%"),
new AttackInfo("Strength","Normal",15,80,"100%"),
new AttackInfo("Absorb","Grass",20,20,"100%"),
new AttackInfo("Mega Drain","Grass",10,40,"100%"),
new AttackInfo("Leech Seed","Grass",10,-1,"90%"),
new AttackInfo("Growth","Normal",40,-1,"-1"),
new AttackInfo("Razor Leaf","Grass",25,55,"95%"),
new AttackInfo("Solar Beam","Grass",10,120,"100%"),
new AttackInfo("Poison Powder","Poison",35,-1,"75%"),
new AttackInfo("Stun Spore","Grass",30,-1,"75%"),
new AttackInfo("Sleep Powder","Grass",15,-1,"75%"),
new AttackInfo("Petal Dance","Grass",20,70,"100%"),
new AttackInfo("String Shot","Bug",40,-1,"95%"),
new AttackInfo("Dragon Rage","Dragon",10,-40,"100%"), //Always deals 40 HP damage
new AttackInfo("Fire Spin","Fire",15,15,"70%"),
new AttackInfo("Thunder Shock","Electric",30,40,"100%"),
new AttackInfo("Thunderbolt","Electric",15,95,"100%"),
new AttackInfo("Thunder Wave","Electric",20,-1,"100%"),
new AttackInfo("Thunder","Electric",10,120,"70%"),
new AttackInfo("Rock Throw","Rock",15,50,"90%"),
new AttackInfo("Earthquake","Ground",10,100,"100%"),
new AttackInfo("Fissure","Ground",5,-1,"Accuracy is ((level of user - level of target) + 30). Cannot hit Pokemon of a higher level."),
new AttackInfo("Dig","Ground",10,60,"100%"),
new AttackInfo("Toxic","Poison",10,-1,"85%"),
new AttackInfo("Confusion","Psychic",25,50,"100%"),
new AttackInfo("Psychic","Psychic",10,90,"100%"),
new AttackInfo("Hypnosis","Psychic",20,-1,"60%"),
new AttackInfo("Meditate","Psychic",40,-1,"-1"),
new AttackInfo("Agility","Psychic",30,-1,"-1"),
new AttackInfo("Quick Attack","Normal",30,40,"100%"),
new AttackInfo("Rage","Normal",20,20,"100%"),
new AttackInfo("Teleport","Psychic",20,-1,"-1"),
new AttackInfo("Night Shade","Ghost",15,-1,"100%"),
new AttackInfo("Mimic","Normal",10,-1,"100%"),
new AttackInfo("Screech","Normal",40,-1,"85%"),
new AttackInfo("Double Team","Normal",15,-1,"-1"),
new AttackInfo("Recover","Normal",20,-1,"-1"),
new AttackInfo("Harden","Normal",30,-1,"-1"),
new AttackInfo("Minimize","Normal",20,-1,"-1"),
new AttackInfo("Smokescreen","Normal",20,-1,"100%"),
new AttackInfo("Confuse Ray","Ghost",10,-1,"100%"),
new AttackInfo("Withdraw","Water",40,-1,"-1"),
new AttackInfo("Defense Curl","Normal",40,-1,"-1"),
new AttackInfo("Barrier","Psychic",30,-1,"-1"),
new AttackInfo("Light Screen","Psychic",30,-1,"-1"),
new AttackInfo("Haze","Ice",30,-1,"-1"),
new AttackInfo("Reflect","Psychic",20,-1,"-1"),
new AttackInfo("Focus Energy","Normal",30,-1,"-1"),
new AttackInfo("Bide","Normal",10,-1,"100%"),
new AttackInfo("Metronome","Normal",10,-1,"-1"),
new AttackInfo("Mirror Move","Flying",20,-1,"-1"),
new AttackInfo("Self-Destruct","Normal",5,200,"100%"),
new AttackInfo("Egg Bomb","Normal",10,100,"75%"),
new AttackInfo("Lick","Ghost",30,20,"100%"),
new AttackInfo("Smog","Poison",20,20,"70%"),
new AttackInfo("Sludge","Poison",20,65,"100%"),
new AttackInfo("Bone Club","Ground",20,65,"85%"),
new AttackInfo("Fire Blast","Fire",5,120,"85%"),
new AttackInfo("Waterfall","Water",15,80,"100%"),
new AttackInfo("Clamp","Water",10,35,"75%"),
new AttackInfo("Swift","Normal",20,60,"-1"),
new AttackInfo("Skull Bash","Normal",15,100,"100%"),
new AttackInfo("Spike Cannon","Normal",15,20,"100%"),
new AttackInfo("Constrict","Normal",35,10,"100%"),
new AttackInfo("Amnesia","Psychic",20,-1,"-1"),
new AttackInfo("Kinesis","Psychic",15,-1,"80%"),
new AttackInfo("Soft-Boiled","Normal",10,-1,"-1"),
new AttackInfo("High Jump Kick","Fighting",20,85,"90%"),
new AttackInfo("Glare","Normal",30,-1,"75%"),
new AttackInfo("Dream Eater","Psychic",15,100,"100%"),
new AttackInfo("Poison Gas","Poison",40,-1,"55%"),
new AttackInfo("Barrage","Normal",20,15,"85%"),
new AttackInfo("Leech Life","Bug",15,20,"100%"),
new AttackInfo("Lovely Kiss","Normal",10,-1,"75%"),
new AttackInfo("Sky Attack","Flying",5,140,"90%"),
new AttackInfo("Transform","Normal",10,-1,"-1"),
new AttackInfo("Bubble","Water",30,20,"100%"),
new AttackInfo("Dizzy Punch","Normal",10,70,"100%"),
new AttackInfo("Spore","Grass",15,-1,"100%"),
new AttackInfo("Flash","Normal",20,-1,"70%"),
new AttackInfo("Psywave","Psychic",15,-1,"80%"),
new AttackInfo("Splash","Normal",40,-1,"-1"),
new AttackInfo("Acid Armor","Poison",40,-1,"-1"),
new AttackInfo("Crabhammer","Water",10,90,"85%"),
new AttackInfo("Explosion","Normal",5,250,"100%"),
new AttackInfo("Fury Swipes","Normal",15,18,"80%"),
new AttackInfo("Bonemerang","Ground",10,50,"90%"),
new AttackInfo("Rest","Psychic",10,-1,"-1"),
new AttackInfo("Rock Slide","Rock",10,75,"90%"),
new AttackInfo("Hyper Fang","Normal",15,80,"90%"),
new AttackInfo("Sharpen","Normal",30,-1,"-1"),
new AttackInfo("Conversion","Normal",30,-1,"-1"),
new AttackInfo("Tri Attack","Normal",10,80,"100%"),
new AttackInfo("Super Fang","Normal",10,-1,"90%"),
new AttackInfo("Slash","Normal",20,70,"100%"),
new AttackInfo("Substitute","Normal",10,-1,"-1"),
new AttackInfo("Struggle","Normal",1,50,"100%"),
new AttackInfo("Sketch","Normal",1,-1,"-1"),
new AttackInfo("Triple Kick","Fighting",10,10,"90%"),
new AttackInfo("Thief","Dark",10,40,"100%"),
new AttackInfo("Spider Web","Bug",10,-1,"-1"),
new AttackInfo("Mind Reader","Normal",5,-1,"100%"),
new AttackInfo("Nightmare","Ghost",15,-1,"100%"),
new AttackInfo("Flame Wheel","Fire",25,60,"100%"),
new AttackInfo("Snore","Normal",15,40,"100%"),
new AttackInfo("Curse","Ghost",10,-1,"-1"),
new AttackInfo("Flail","Normal",15,-1,"100%"),
new AttackInfo("Conversion 2","Normal",30,-1,"-1"),
new AttackInfo("Aeroblast","Flying",5,100,"95%"),
new AttackInfo("Cotton Spore","Grass",40,-1,"85%"),
new AttackInfo("Reversal","Fighting",15,-1,"100%"),
new AttackInfo("Spite","Ghost",10,-1,"100%"),
new AttackInfo("Powder Snow","Ice",25,40,"100%"),
new AttackInfo("Protect","Normal",10,-1,"-1"),
new AttackInfo("Mach Punch","Fighting",30,40,"100%"),
new AttackInfo("Scary Face","Normal",10,-1,"90%"),
new AttackInfo("Feint Attack","Dark",20,60,"-1"),
new AttackInfo("Sweet Kiss","Fairy",10,-1,"75%"),
new AttackInfo("Belly Drum","Normal",10,-1,"-1"),
new AttackInfo("Sludge Bomb","Poison",10,90,"100%"),
new AttackInfo("Mud-Slap","Ground",10,20,"100%"),
new AttackInfo("Octazooka","Water",10,65,"85%"),
new AttackInfo("Spikes","Ground",20,-1,"-1"),
new AttackInfo("Zap Cannon","Electric",5,100,"50%"),
new AttackInfo("Foresight","Normal",40,-1,"100%"),
new AttackInfo("Destiny Bond","Ghost",5,-1,"-1"),
new AttackInfo("Perish Song","Normal",5,-1,"-1"),
new AttackInfo("Icy Wind","Ice",15,55,"95%"),
new AttackInfo("Detect","Fighting",5,-1,"-1"),
new AttackInfo("Bone Rush","Ground",10,25,"80%"),
new AttackInfo("Lock-On","Normal",5,-1,"100%"),
new AttackInfo("Outrage","Dragon",15,90,"100%"),
new AttackInfo("Sandstorm","Rock",10,-1,"-1"),
new AttackInfo("Giga Drain","Grass",5,60,"100%"),
new AttackInfo("Endure","Normal",10,-1,"-1"),
new AttackInfo("Charm","Fairy",20,-1,"100%"),
new AttackInfo("Rollout","Rock",20,30,"90%"),
new AttackInfo("False Swipe","Normal",40,40,"100%"),
new AttackInfo("Swagger","Normal",15,-1,"90%"),
new AttackInfo("Milk Drink","Normal",10,-1,"-1"),
new AttackInfo("Spark","Electric",20,65,"100%"),
new AttackInfo("Fury Cutter","Bug",20,10,"95%"),
new AttackInfo("Steel Wing","Steel",25,70,"90%"),
new AttackInfo("Mean Look","Normal",5,-1,"-1"),
new AttackInfo("Attract","Normal",15,-1,"100%"),
new AttackInfo("Sleep Talk","Normal",10,-1,"-1"),
new AttackInfo("Heal Bell","Normal",5,-1,"-1"),
new AttackInfo("Return","Normal",20,-1,"100%"),
new AttackInfo("Present","Normal",15,-1,"90%"),
new AttackInfo("Frustration","Normal",20,-1,"100%"),
new AttackInfo("Safeguard","Normal",25,-1,"-1"),
new AttackInfo("Pain Split","Normal",20,-1,"100%"),
new AttackInfo("Sacred Fire","Fire",5,100,"95%"),
new AttackInfo("Magnitude","Ground",30,-1,"100%"),
new AttackInfo("Dynamic Punch","Fighting",5,100,"50%"),
new AttackInfo("Megahorn","Bug",10,120,"85%"),
new AttackInfo("Dragon Breath","Dragon",20,60,"100%"),
new AttackInfo("Baton Pass","Normal",40,-1,"-1"),
new AttackInfo("Encore","Normal",5,-1,"100%"),
new AttackInfo("Pursuit","Dark",20,40,"100%"),
new AttackInfo("Rapid Spin","Normal",40,20,"100%"),
new AttackInfo("Sweet Scent","Normal",20,-1,"100%"),
new AttackInfo("Iron Tail","Steel",15,100,"75%"),
new AttackInfo("Metal Claw","Steel",35,50,"95%"),
new AttackInfo("Vital Throw","Fighting",10,70,"-1"),
new AttackInfo("Morning Sun","Normal",5,-1,"-1"),
new AttackInfo("Synthesis","Grass",5,-1,"-1"),
new AttackInfo("Moonlight","Fairy",5,-1,"-1"),
new AttackInfo("Hidden Power","Normal",15,60,"100%"),
new AttackInfo("Cross Chop","Fighting",5,100,"80%"),
new AttackInfo("Twister","Dragon",20,40,"100%"),
new AttackInfo("Rain Dance","Water",5,-1,"-1"),
new AttackInfo("Sunny Day","Fire",5,-1,"-1"),
new AttackInfo("Crunch","Dark",15,80,"100%"),
new AttackInfo("Mirror Coat","Psychic",20,-1,"100%"),
new AttackInfo("Psych Up","Normal",10,-1,"-1"),
new AttackInfo("Extreme Speed","Normal",5,80,"100%"),
new AttackInfo("Ancient Power","Rock",5,60,"100%"),
new AttackInfo("Shadow Ball","Ghost",15,80,"100%"),
new AttackInfo("Future Sight","Psychic",15,80,"90%"),
new AttackInfo("Rock Smash","Fighting",15,20,"100%"),
new AttackInfo("Whirlpool","Water",15,15,"70%"),
new AttackInfo("Beat Up","Dark",10,10,"100%"),
new AttackInfo("Fake Out","Normal",10,40,"100%")
        };

        private static Dictionary<int, PokemonInfo> IDtoPokemonInfo = new Dictionary<int, PokemonInfo>();

        private static readonly List<PokemonInfo> PokemonInfo = new List<PokemonInfo>()
        {
            new PokemonInfo(0,0,0,0,0,0,"???",0,"Medium_Slow"),
new PokemonInfo(25,20,15,105,55,90,"Abra",63,"Medium_Slow"),
new PokemonInfo(80,105,65,60,75,130,"Aerodactyl",142,"Slow"),
new PokemonInfo(55,70,55,40,55,85,"Aipom",190,"Fast"),
new PokemonInfo(55,50,45,135,85,120,"Alakazam",65,"Medium_Slow"),
new PokemonInfo(90,75,75,115,90,55,"Ampharos",181,"Medium_Slow"),
new PokemonInfo(60,85,69,65,79,80,"Arbok",24,"Medium_Fast"),
new PokemonInfo(90,110,80,100,80,95,"Arcanine",59,"Slow"),
new PokemonInfo(70,90,70,60,60,40,"Ariados",168,"Fast"),
new PokemonInfo(90,85,100,95,125,85,"Articuno",144,"Slow"),
new PokemonInfo(100,50,80,50,80,50,"Azumarill",184,"Fast"),
new PokemonInfo(60,62,80,63,80,60,"Bayleef",153,"Medium_Slow"),
new PokemonInfo(65,80,40,45,80,75,"Beedrill",15,"Medium_Fast"),
new PokemonInfo(75,80,85,90,100,50,"Bellossom",182,"Medium_Slow"),
new PokemonInfo(50,75,35,70,30,40,"Bellsprout",69,"Medium_Slow"),
new PokemonInfo(79,83,100,85,105,78,"Blastoise",9,"Medium_Slow"),
new PokemonInfo(255,10,10,75,135,55,"Blissey",242,"Fast"),
new PokemonInfo(45,49,49,65,65,45,"Bulbasaur",1,"Medium_Slow"),
new PokemonInfo(60,45,50,80,80,70,"Butterfree",12,"Medium_Fast"),
new PokemonInfo(45,30,35,20,20,45,"Caterpie",10,"Medium_Fast"),
new PokemonInfo(100,100,100,100,100,100,"Celebi",251,"Medium_Slow"),
new PokemonInfo(250,5,5,35,105,50,"Chansey",113,"Fast"),
new PokemonInfo(78,84,78,109,85,100,"Charizard",6,"Medium_Slow"),
new PokemonInfo(39,52,43,60,50,65,"Charmander",4,"Medium_Slow"),
new PokemonInfo(58,64,58,80,65,80,"Charmeleon",5,"Medium_Slow"),
new PokemonInfo(45,49,65,49,65,45,"Chikorita",152,"Medium_Slow"),
new PokemonInfo(75,38,38,56,56,67,"Chinchou",170,"Slow"),
new PokemonInfo(95,70,73,85,90,60,"Clefable",36,"Fast"),
new PokemonInfo(70,45,48,60,65,35,"Clefairy",35,"Fast"),
new PokemonInfo(50,25,28,45,55,15,"Cleffa",173,"Fast"),
new PokemonInfo(50,95,180,85,45,70,"Cloyster",91,"Slow"),
new PokemonInfo(55,55,85,65,85,35,"Corsola",222,"Fast"),
new PokemonInfo(85,90,80,70,80,130,"Crobat",169,"Medium_Fast"),
new PokemonInfo(65,80,80,59,63,58,"Croconaw",159,"Medium_Slow"),
new PokemonInfo(50,50,95,40,50,35,"Cubone",104,"Medium_Fast"),
new PokemonInfo(39,52,43,60,50,65,"Cyndaquil",155,"Medium_Slow"),
new PokemonInfo(45,55,45,65,45,75,"Delibird",225,"Fast"),
new PokemonInfo(90,70,80,70,95,70,"Dewgong",87,"Medium_Fast"),
new PokemonInfo(10,55,25,35,45,95,"Diglett",50,"Medium_Fast"),
new PokemonInfo(48,48,48,48,48,48,"Ditto",132,"Medium_Fast"),
new PokemonInfo(60,110,70,60,60,100,"Dodrio",85,"Medium_Fast"),
new PokemonInfo(35,85,45,35,35,75,"Doduo",84,"Medium_Fast"),
new PokemonInfo(90,120,120,60,60,50,"Donphan",232,"Medium_Fast"),
new PokemonInfo(61,84,65,70,70,70,"Dragonair",148,"Slow"),
new PokemonInfo(91,134,95,100,100,80,"Dragonite",149,"Slow"),
new PokemonInfo(41,64,45,50,50,50,"Dratini",147,"Slow"),
new PokemonInfo(60,48,45,43,90,42,"Drowzee",96,"Medium_Fast"),
new PokemonInfo(35,80,50,50,70,120,"Dugtrio",51,"Medium_Fast"),
new PokemonInfo(100,70,70,65,65,45,"Dunsparce",206,"Medium_Fast"),
new PokemonInfo(55,55,50,45,65,55,"Eevee",133,"Medium_Fast"),
new PokemonInfo(35,60,44,40,54,55,"Ekans",23,"Medium_Fast"),
new PokemonInfo(65,83,57,95,85,105,"Electabuzz",125,"Medium_Fast"),
new PokemonInfo(60,50,70,80,80,140,"Electrode",101,"Medium_Fast"),
new PokemonInfo(45,63,37,65,55,95,"Elekid",239,"Medium_Fast"),
new PokemonInfo(115,115,85,90,75,100,"Entei",244,"Slow"),
new PokemonInfo(65,65,60,130,95,110,"Espeon",196,"Medium_Fast"),
new PokemonInfo(60,40,80,60,45,40,"Exeggcute",102,"Slow"),
new PokemonInfo(95,95,85,125,65,55,"Exeggutor",103,"Slow"),
new PokemonInfo(52,65,55,58,62,60,"Farfetchd",83,"Medium_Fast"),
new PokemonInfo(65,90,65,61,61,100,"Fearow",22,"Medium_Fast"),
new PokemonInfo(85,105,100,79,83,78,"Feraligatr",160,"Medium_Slow"),
new PokemonInfo(70,55,55,80,60,45,"Flaaffy",180,"Medium_Slow"),
new PokemonInfo(65,130,60,95,110,65,"Flareon",136,"Medium_Fast"),
new PokemonInfo(75,90,140,60,60,40,"Forretress",205,"Medium_Fast"),
new PokemonInfo(85,76,64,45,55,90,"Furret",162,"Medium_Fast"),
new PokemonInfo(30,35,30,100,35,80,"Gastly",92,"Medium_Slow"),
new PokemonInfo(60,65,60,130,75,110,"Gengar",94,"Medium_Slow"),
new PokemonInfo(40,80,100,30,30,20,"Geodude",74,"Medium_Slow"),
new PokemonInfo(70,80,65,90,65,85,"Girafarig",203,"Medium_Fast"),
new PokemonInfo(65,75,105,35,65,85,"Gligar",207,"Medium_Slow"),
new PokemonInfo(60,65,70,85,75,40,"Gloom",44,"Medium_Slow"),
new PokemonInfo(75,80,70,65,75,90,"Golbat",42,"Medium_Fast"),
new PokemonInfo(45,67,60,35,50,63,"Goldeen",118,"Medium_Fast"),
new PokemonInfo(80,82,78,95,80,85,"Golduck",55,"Medium_Fast"),
new PokemonInfo(80,110,130,55,65,45,"Golem",76,"Medium_Slow"),
new PokemonInfo(90,120,75,60,60,45,"Granbull",210,"Fast"),
new PokemonInfo(55,95,115,45,45,35,"Graveler",75,"Medium_Slow"),
new PokemonInfo(80,80,50,40,50,25,"Grimer",88,"Medium_Fast"),
new PokemonInfo(55,70,45,70,50,60,"Growlithe",58,"Slow"),
new PokemonInfo(95,125,79,60,100,81,"Gyrados",130,"Slow"),
new PokemonInfo(45,50,45,115,55,95,"Haunter",93,"Medium_Slow"),
new PokemonInfo(80,125,75,40,95,85,"Heracross",214,"Slow"),
new PokemonInfo(50,105,79,35,110,76,"Hitmonchan",107,"Medium_Fast"),
new PokemonInfo(50,120,53,35,110,87,"Hitmonlee",106,"Medium_Fast"),
new PokemonInfo(50,95,95,35,110,70,"Hitmontop",237,"Medium_Fast"),
new PokemonInfo(106,130,90,110,154,90,"Ho-Oh",250,"Slow"),
new PokemonInfo(60,30,30,36,56,50,"Hoothoot",163,"Medium_Fast"),
new PokemonInfo(35,35,40,35,55,50,"Hoppip",187,"Medium_Slow"),
new PokemonInfo(30,40,70,70,25,60,"Horsea",116,"Medium_Fast"),
new PokemonInfo(75,90,50,110,80,95,"Houndoom",229,"Slow"),
new PokemonInfo(45,60,30,80,50,65,"Houndour",228,"Slow"),
new PokemonInfo(85,73,70,73,115,67,"Hypno",97,"Medium_Fast"),
new PokemonInfo(90,30,15,40,20,15,"Igglybuff",174,"Fast"),
new PokemonInfo(60,62,63,80,80,60,"Ivysaur",2,"Medium_Slow"),
new PokemonInfo(115,45,20,45,25,20,"Jigglypuff",39,"Fast"),
new PokemonInfo(65,65,60,110,95,130,"Jolteon",135,"Medium_Fast"),
new PokemonInfo(75,55,70,55,85,110,"Jumpluff",189,"Medium_Slow"),
new PokemonInfo(65,50,35,115,95,95,"Jynx",124,"Medium_Fast"),
new PokemonInfo(30,80,90,55,45,55,"Kabuto",140,"Medium_Fast"),
new PokemonInfo(60,115,105,65,70,80,"Kabutops",141,"Medium_Fast"),
new PokemonInfo(40,35,30,120,70,105,"Kadabra",64,"Medium_Slow"),
new PokemonInfo(45,25,50,25,25,35,"Kakuna",14,"Medium_Fast"),
new PokemonInfo(105,95,80,40,80,90,"Kangaskhan",115,"Medium_Fast"),
new PokemonInfo(75,95,95,95,95,85,"Kingdra",230,"Medium_Fast"),
new PokemonInfo(55,130,115,50,50,75,"Kingler",99,"Medium_Fast"),
new PokemonInfo(40,65,95,60,45,35,"Koffing",109,"Medium_Fast"),
new PokemonInfo(30,105,90,25,25,50,"Krabby",98,"Medium_Fast"),
new PokemonInfo(125,58,58,76,76,67,"Lanturn",171,"Slow"),
new PokemonInfo(130,85,80,85,95,60,"Lapras",131,"Slow"),
new PokemonInfo(50,64,50,45,50,41,"Larvitar",246,"Slow"),
new PokemonInfo(55,35,50,55,110,85,"Ledian",166,"Fast"),
new PokemonInfo(40,20,30,40,80,55,"Ledyba",165,"Fast"),
new PokemonInfo(90,55,75,60,75,30,"Lickitung",108,"Medium_Fast"),
new PokemonInfo(106,90,130,90,154,110,"Lugia",249,"Slow"),
new PokemonInfo(90,130,80,65,85,55,"Machamp",68,"Medium_Slow"),
new PokemonInfo(80,100,70,50,60,45,"Machoke",67,"Medium_Slow"),
new PokemonInfo(70,80,50,35,35,35,"Machop",66,"Medium_Slow"),
new PokemonInfo(45,75,37,70,55,83,"Magby",240,"Medium_Fast"),
new PokemonInfo(50,50,120,80,80,30,"Magcargo",219,"Medium_Fast"),
new PokemonInfo(20,10,55,15,20,80,"Magikarp",129,"Slow"),
new PokemonInfo(65,95,57,100,85,93,"Magmar",126,"Medium_Fast"),
new PokemonInfo(25,35,70,95,55,45,"Magnemite",81,"Medium_Fast"),
new PokemonInfo(50,60,95,120,70,70,"Magneton",82,"Medium_Fast"),
new PokemonInfo(40,80,35,35,45,70,"Mankey",56,"Medium_Fast"),
new PokemonInfo(65,40,70,80,140,70,"Mantine",226,"Slow"),
new PokemonInfo(55,40,40,65,45,35,"Mareep",179,"Medium_Slow"),
new PokemonInfo(70,20,50,20,50,40,"Marill",183,"Fast"),
new PokemonInfo(60,80,110,50,80,45,"Marowak",105,"Medium_Fast"),
new PokemonInfo(80,82,100,83,100,80,"Meganium",154,"Medium_Slow"),
new PokemonInfo(40,45,35,40,40,90,"Meowth",52,"Medium_Fast"),
new PokemonInfo(50,20,55,25,25,30,"Metapod",11,"Medium_Fast"),
new PokemonInfo(100,100,100,100,100,100,"Mew",151,"Medium_Slow"),
new PokemonInfo(106,110,90,154,90,130,"Mewtwo",150,"Slow"),
new PokemonInfo(95,80,105,40,70,100,"Miltank",241,"Slow"),
new PokemonInfo(60,60,60,85,85,85,"Misdreavus",200,"Fast"),
new PokemonInfo(90,100,90,125,85,90,"Moltres",146,"Slow"),
new PokemonInfo(40,45,65,100,120,90,"Mr. Mime",122,"Medium_Fast"),
new PokemonInfo(105,105,75,65,100,50,"Muk",89,"Medium_Fast"),
new PokemonInfo(60,85,42,85,42,91,"Murkrow",198,"Medium_Slow"),
new PokemonInfo(40,50,45,70,45,70,"Natu",177,"Medium_Fast"),
new PokemonInfo(81,92,77,85,75,85,"Nidoking",34,"Medium_Slow"),
new PokemonInfo(90,82,87,75,85,76,"Nidoqueen",31,"Medium_Slow"),
new PokemonInfo(55,47,52,40,40,41,"Nidoran♀",29,"Medium_Slow"),
new PokemonInfo(46,57,40,40,40,50,"Nidoran♂",32,"Medium_Slow"),
new PokemonInfo(70,62,67,55,55,56,"Nidorina",30,"Medium_Slow"),
new PokemonInfo(61,72,57,55,55,65,"Nidorino",33,"Medium_Slow"),
new PokemonInfo(73,76,75,81,100,100,"Ninetales",38,"Medium_Fast"),
new PokemonInfo(100,50,50,76,96,70,"Noctowl",164,"Medium_Fast"),
new PokemonInfo(75,105,75,105,75,45,"Octillery",224,"Medium_Fast"),
new PokemonInfo(45,50,55,75,65,30,"Oddish",43,"Medium_Slow"),
new PokemonInfo(35,40,100,90,55,35,"Omanyte",138,"Medium_Fast"),
new PokemonInfo(70,60,125,115,70,55,"Omastar",139,"Medium_Fast"),
new PokemonInfo(35,45,160,30,45,70,"Onix",95,"Medium_Fast"),
new PokemonInfo(35,70,55,45,55,25,"Paras",46,"Medium_Fast"),
new PokemonInfo(60,95,80,60,80,30,"Parasect",47,"Medium_Fast"),
new PokemonInfo(65,70,60,65,65,115,"Persian",53,"Medium_Fast"),
new PokemonInfo(90,60,60,40,40,40,"Phanpy",231,"Medium_Fast"),
new PokemonInfo(20,40,15,35,35,60,"Pichu",172,"Medium_Fast"),
new PokemonInfo(83,80,75,70,70,91,"Pidgeot",18,"Medium_Slow"),
new PokemonInfo(63,60,55,50,50,71,"Pidgeotto",17,"Medium_Slow"),
new PokemonInfo(40,45,40,35,35,56,"Pidgey",16,"Medium_Slow"),
new PokemonInfo(35,55,30,50,40,90,"Pikachu",25,"Medium_Fast"),
new PokemonInfo(100,100,80,60,60,50,"Piloswine",221,"Slow"),
new PokemonInfo(50,65,90,35,35,15,"Pineco",204,"Medium_Fast"),
new PokemonInfo(65,125,100,55,70,85,"Pinsir",127,"Slow"),
new PokemonInfo(90,75,75,90,100,70,"Politoed",186,"Medium_Slow"),
new PokemonInfo(40,50,40,40,40,90,"Poliwag",60,"Medium_Slow"),
new PokemonInfo(65,65,65,5,50,90,"Poliwhirl",61,"Medium_Slow"),
new PokemonInfo(90,85,95,70,90,70,"Poliwrath",62,"Medium_Slow"),
new PokemonInfo(50,85,55,65,65,90,"Ponyta",77,"Medium_Fast"),
new PokemonInfo(65,60,70,85,75,40,"Porygon",137,"Medium_Fast"),
new PokemonInfo(85,80,90,105,95,60,"Porygon2",233,"Medium_Fast"),
new PokemonInfo(65,105,60,60,70,95,"Primeape",57,"Medium_Fast"),
new PokemonInfo(50,52,48,65,50,55,"Psyduck",54,"Medium_Fast"),
new PokemonInfo(70,84,70,65,70,51,"Pupitar",247,"Slow"),
new PokemonInfo(95,85,85,65,65,35,"Quagsire",195,"Medium_Fast"),
new PokemonInfo(58,64,58,80,65,80,"Quilava",156,"Medium_Slow"),
new PokemonInfo(65,95,75,55,55,85,"Qwilfish",211,"Medium_Fast"),
new PokemonInfo(60,90,55,90,80,100,"Raichu",26,"Medium_Fast"),
new PokemonInfo(90,85,75,115,100,115,"Raikou",243,"Slow"),
new PokemonInfo(65,100,70,80,80,105,"Rapidash",78,"Medium_Fast"),
new PokemonInfo(55,81,60,50,70,97,"Raticate",20,"Medium_Fast"),
new PokemonInfo(30,56,35,25,35,72,"Rattata",19,"Medium_Fast"),
new PokemonInfo(35,65,35,65,35,65,"Remoraid",223,"Medium_Fast"),
new PokemonInfo(105,130,120,45,45,40,"Rhydon",112,"Slow"),
new PokemonInfo(80,85,95,30,30,25,"Rhyhorn",111,"Slow"),
new PokemonInfo(50,75,85,20,30,40,"Sandshrew",27,"Medium_Fast"),
new PokemonInfo(75,100,110,45,55,65,"Sandslash",28,"Medium_Fast"),
new PokemonInfo(70,130,100,55,80,65,"Scizor",212,"Medium_Fast"),
new PokemonInfo(70,110,80,55,80,105,"Scyther",123,"Medium_Fast"),
new PokemonInfo(55,65,95,95,45,85,"Seadra",117,"Medium_Fast"),
new PokemonInfo(80,92,65,65,80,68,"Seaking",119,"Medium_Fast"),
new PokemonInfo(65,45,55,45,70,45,"Seel",86,"Medium_Fast"),
new PokemonInfo(35,46,34,35,45,20,"Sentret",161,"Medium_Fast"),
new PokemonInfo(30,65,100,45,25,40,"Shellder",90,"Slow"),
new PokemonInfo(20,10,230,10,230,5,"Shuckle",213,"Medium_Slow"),
new PokemonInfo(65,80,140,40,70,70,"Skarmory",227,"Slow"),
new PokemonInfo(55,45,50,45,65,80,"Skiploom",188,"Medium_Slow"),
new PokemonInfo(95,75,110,100,80,30,"Slowbro",80,"Medium_Fast"),
new PokemonInfo(95,75,80,100,80,30,"Slowking",199,"Medium_Fast"),
new PokemonInfo(90,65,65,40,40,15,"Slowpoke",79,"Medium_Fast"),
new PokemonInfo(40,40,40,70,40,20,"Slugma",218,"Medium_Fast"),
new PokemonInfo(55,20,35,20,45,75,"Smeargle",235,"Fast"),
new PokemonInfo(45,30,15,85,65,65,"Smoochum",238,"Medium_Fast"),
new PokemonInfo(55,95,55,35,75,115,"Sneasel",215,"Medium_Slow"),
new PokemonInfo(160,110,65,65,110,30,"Snorlax",143,"Slow"),
new PokemonInfo(60,80,50,40,40,30,"Snubbull",209,"Fast"),
new PokemonInfo(40,60,30,31,31,70,"Spearow",21,"Medium_Fast"),
new PokemonInfo(40,60,40,40,40,30,"Spinarak",167,"Fast"),
new PokemonInfo(44,48,65,50,64,43,"Squirtle",7,"Medium_Slow"),
new PokemonInfo(73,95,62,85,65,85,"Stantler",234,"Slow"),
new PokemonInfo(60,75,85,100,85,115,"Starmie",121,"Slow"),
new PokemonInfo(30,45,55,70,55,85,"Staryu",120,"Slow"),
new PokemonInfo(75,85,200,55,65,30,"Steelix",208,"Medium_Fast"),
new PokemonInfo(70,100,115,30,65,30,"Sudowoodo",185,"Medium_Fast"),
new PokemonInfo(100,75,115,90,115,85,"Suicune",245,"Slow"),
new PokemonInfo(75,75,55,105,85,30,"Sunflora",192,"Medium_Slow"),
new PokemonInfo(30,30,30,30,30,30,"Sunkern",191,"Medium_Slow"),
new PokemonInfo(50,50,40,30,30,50,"Swinub",220,"Slow"),
new PokemonInfo(65,55,115,100,40,60,"Tangela",114,"Medium_Fast"),
new PokemonInfo(75,100,95,40,70,110,"Tauros",128,"Slow"),
new PokemonInfo(60,80,50,50,50,40,"Teddiursa",216,"Medium_Fast"),
new PokemonInfo(40,40,35,50,100,70,"Tentacool",72,"Slow"),
new PokemonInfo(80,70,65,80,120,100,"Tentacruel",73,"Slow"),
new PokemonInfo(35,20,65,40,65,20,"Togepi",175,"Fast"),
new PokemonInfo(55,40,85,80,105,40,"Togetic",176,"Fast"),
new PokemonInfo(50,65,64,44,48,43,"Totodile",158,"Medium_Slow"),
new PokemonInfo(78,84,78,109,85,100,"Typhlosion",157,"Medium_Slow"),
new PokemonInfo(100,134,110,95,100,61,"Tyranitar",248,"Slow"),
new PokemonInfo(35,35,35,35,35,35,"Tyrogue",236,"Medium_Fast"),
new PokemonInfo(95,65,110,60,130,65,"Umbreon",197,"Medium_Fast"),
new PokemonInfo(48,72,48,72,48,48,"Unown",201,"Medium_Fast"),
new PokemonInfo(90,130,75,75,75,55,"Ursaring",217,"Medium_Fast"),
new PokemonInfo(130,65,60,110,95,65,"Vaporeon",134,"Medium_Fast"),
new PokemonInfo(70,65,60,90,75,90,"Venomoth",49,"Medium_Fast"),
new PokemonInfo(60,55,50,40,55,45,"Venonat",48,"Medium_Fast"),
new PokemonInfo(80,82,83,100,100,80,"Venusaur",3,"Medium_Slow"),
new PokemonInfo(80,105,65,100,60,70,"Victreebel",71,"Medium_Slow"),
new PokemonInfo(75,80,85,100,90,50,"Vileplume",45,"Medium_Slow"),
new PokemonInfo(40,30,50,55,55,100,"Voltorb",100,"Medium_Fast"),
new PokemonInfo(38,41,40,50,65,65,"Vulpix",37,"Medium_Fast"),
new PokemonInfo(59,63,80,65,80,58,"Wartortle",8,"Medium_Slow"),
new PokemonInfo(40,35,30,20,20,50,"Weedle",13,"Medium_Fast"),
new PokemonInfo(65,90,50,85,45,55,"Weepinbell",70,"Medium_Slow"),
new PokemonInfo(65,90,120,85,70,60,"Weezing",110,"Medium_Fast"),
new PokemonInfo(140,70,45,75,50,45,"Wigglytuff",40,"Fast"),
new PokemonInfo(190,33,58,33,58,33,"Wobbuffet",202,"Medium_Fast"),
new PokemonInfo(55,45,45,25,25,15,"Wooper",194,"Medium_Fast"),
new PokemonInfo(65,75,70,95,70,95,"Xatu",178,"Medium_Fast"),
new PokemonInfo(65,65,45,75,45,95,"Yanma",193,"Medium_Fast"),
new PokemonInfo(90,90,85,125,90,100,"Zapdos",145,"Slow"),
new PokemonInfo(40,45,35,30,40,55,"Zubat",41,"Medium_Fast")
        };

        public static PokemonInfo GetPokemonInfoFromID(int id)
        {
            if(IDtoPokemonInfo.Count == 0)
                foreach(var p in PokemonInfo)
                    IDtoPokemonInfo.Add(p.ID, p);

            if (IDtoPokemonInfo.ContainsKey(id))
                return IDtoPokemonInfo[id];

            return null;
        }


        public static AttackInfo GetAttackInfo(int id)
        {
            if (id < 0 || id > AttackInfo.Count)
                return null;

            return AttackInfo[id];
        }
    }
}
