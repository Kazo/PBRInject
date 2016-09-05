using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Web.Script.Serialization;

namespace PBRInject
{
    class Player
    {
        public Pokemon[] player1 { get; set; }
        public Pokemon[] player2 { get; set; }
    }

    class Pokemon
    {
        public Species species { get; set; }
        public Item item { get; set; }
        public Ability ability { get; set; }
        public Nature nature { get; set; }
        public Move[] moves { get; set; }
        public Stats ivs { get; set; }
        public Stats evs { get; set; }
    }

    class Species
    {
        public UInt16 id { get; set; }
        public String gender { get; set; }
        public Byte form { get; set; }
        public Boolean shiny { get; set; }
    }

    class Item
    {
        public UInt16 id { get; set; }
    }

    class Ability
    {
        public Byte id { get; set; }
    }

    class Nature
    {
        public Byte id { get; set; }
    }

    class Move
    {
        public UInt16 id { get; set; }
    }

    class Stats
    {
        public Byte hp { get; set; }
        public Byte atk { get; set; }
        public Byte def { get; set; }
        public Byte spa { get; set; }
        public Byte spd { get; set; }
        public Byte spe { get; set; }
    }

    class Program
    {
        static String[] Names = { "", "BULBASAUR", "IVYSAUR", "VENUSAUR", "CHARMANDER", "CHARMELEON", "CHARIZARD", "SQUIRTLE", "WARTORTLE", "BLASTOISE", "CATERPIE", "METAPOD", "BUTTERFREE", "WEEDLE", "KAKUNA", "BEEDRILL", "PIDGEY", "PIDGEOTTO", "PIDGEOT", "RATTATA", "RATICATE", "SPEAROW", "FEAROW", "EKANS", "ARBOK", "PIKACHU", "RAICHU", "SANDSHREW", "SANDSLASH", "NIDORAN♀", "NIDORINA", "NIDOQUEEN", "NIDORAN♂", "NIDORINO", "NIDOKING", "CLEFAIRY", "CLEFABLE", "VULPIX", "NINETALES", "JIGGLYPUFF", "WIGGLYTUFF", "ZUBAT", "GOLBAT", "ODDISH", "GLOOM", "VILEPLUME", "PARAS", "PARASECT", "VENONAT", "VENOMOTH", "DIGLETT", "DUGTRIO", "MEOWTH", "PERSIAN", "PSYDUCK", "GOLDUCK", "MANKEY", "PRIMEAPE", "GROWLITHE", "ARCANINE", "POLIWAG", "POLIWHIRL", "POLIWRATH", "ABRA", "KADABRA", "ALAKAZAM", "MACHOP", "MACHOKE", "MACHAMP", "BELLSPROUT", "WEEPINBELL", "VICTREEBEL", "TENTACOOL", "TENTACRUEL", "GEODUDE", "GRAVELER", "GOLEM", "PONYTA", "RAPIDASH", "SLOWPOKE", "SLOWBRO", "MAGNEMITE", "MAGNETON", "FARFETCH'D", "DODUO", "DODRIO", "SEEL", "DEWGONG", "GRIMER", "MUK", "SHELLDER", "CLOYSTER", "GASTLY", "HAUNTER", "GENGAR", "ONIX", "DROWZEE", "HYPNO", "KRABBY", "KINGLER", "VOLTORB", "ELECTRODE", "EXEGGCUTE", "EXEGGUTOR", "CUBONE", "MAROWAK", "HITMONLEE", "HITMONCHAN", "LICKITUNG", "KOFFING", "WEEZING", "RHYHORN", "RHYDON", "CHANSEY", "TANGELA", "KANGASKHAN", "HORSEA", "SEADRA", "GOLDEEN", "SEAKING", "STARYU", "STARMIE", "MR. MIME", "SCYTHER", "JYNX", "ELECTABUZZ", "MAGMAR", "PINSIR", "TAUROS", "MAGIKARP", "GYARADOS", "LAPRAS", "DITTO", "EEVEE", "VAPOREON", "JOLTEON", "FLAREON", "PORYGON", "OMANYTE", "OMASTAR", "KABUTO", "KABUTOPS", "AERODACTYL", "SNORLAX", "ARTICUNO", "ZAPDOS", "MOLTRES", "DRATINI", "DRAGONAIR", "DRAGONITE", "MEWTWO", "MEW", "CHIKORITA", "BAYLEEF", "MEGANIUM", "CYNDAQUIL", "QUILAVA", "TYPHLOSION", "TOTODILE", "CROCONAW", "FERALIGATR", "SENTRET", "FURRET", "HOOTHOOT", "NOCTOWL", "LEDYBA", "LEDIAN", "SPINARAK", "ARIADOS", "CROBAT", "CHINCHOU", "LANTURN", "PICHU", "CLEFFA", "IGGLYBUFF", "TOGEPI", "TOGETIC", "NATU", "XATU", "MAREEP", "FLAAFFY", "AMPHAROS", "BELLOSSOM", "MARILL", "AZUMARILL", "SUDOWOODO", "POLITOED", "HOPPIP", "SKIPLOOM", "JUMPLUFF", "AIPOM", "SUNKERN", "SUNFLORA", "YANMA", "WOOPER", "QUAGSIRE", "ESPEON", "UMBREON", "MURKROW", "SLOWKING", "MISDREAVUS", "UNOWN", "WOBBUFFET", "GIRAFARIG", "PINECO", "FORRETRESS", "DUNSPARCE", "GLIGAR", "STEELIX", "SNUBBULL", "GRANBULL", "QWILFISH", "SCIZOR", "SHUCKLE", "HERACROSS", "SNEASEL", "TEDDIURSA", "URSARING", "SLUGMA", "MAGCARGO", "SWINUB", "PILOSWINE", "CORSOLA", "REMORAID", "OCTILLERY", "DELIBIRD", "MANTINE", "SKARMORY", "HOUNDOUR", "HOUNDOOM", "KINGDRA", "PHANPY", "DONPHAN", "PORYGON2", "STANTLER", "SMEARGLE", "TYROGUE", "HITMONTOP", "SMOOCHUM", "ELEKID", "MAGBY", "MILTANK", "BLISSEY", "RAIKOU", "ENTEI", "SUICUNE", "LARVITAR", "PUPITAR", "TYRANITAR", "LUGIA", "HO-OH", "CELEBI", "TREECKO", "GROVYLE", "SCEPTILE", "TORCHIC", "COMBUSKEN", "BLAZIKEN", "MUDKIP", "MARSHTOMP", "SWAMPERT", "POOCHYENA", "MIGHTYENA", "ZIGZAGOON", "LINOONE", "WURMPLE", "SILCOON", "BEAUTIFLY", "CASCOON", "DUSTOX", "LOTAD", "LOMBRE", "LUDICOLO", "SEEDOT", "NUZLEAF", "SHIFTRY", "TAILLOW", "SWELLOW", "WINGULL", "PELIPPER", "RALTS", "KIRLIA", "GARDEVOIR", "SURSKIT", "MASQUERAIN", "SHROOMISH", "BRELOOM", "SLAKOTH", "VIGOROTH", "SLAKING", "NINCADA", "NINJASK", "SHEDINJA", "WHISMUR", "LOUDRED", "EXPLOUD", "MAKUHITA", "HARIYAMA", "AZURILL", "NOSEPASS", "SKITTY", "DELCATTY", "SABLEYE", "MAWILE", "ARON", "LAIRON", "AGGRON", "MEDITITE", "MEDICHAM", "ELECTRIKE", "MANECTRIC", "PLUSLE", "MINUN", "VOLBEAT", "ILLUMISE", "ROSELIA", "GULPIN", "SWALOT", "CARVANHA", "SHARPEDO", "WAILMER", "WAILORD", "NUMEL", "CAMERUPT", "TORKOAL", "SPOINK", "GRUMPIG", "SPINDA", "TRAPINCH", "VIBRAVA", "FLYGON", "CACNEA", "CACTURNE", "SWABLU", "ALTARIA", "ZANGOOSE", "SEVIPER", "LUNATONE", "SOLROCK", "BARBOACH", "WHISCASH", "CORPHISH", "CRAWDAUNT", "BALTOY", "CLAYDOL", "LILEEP", "CRADILY", "ANORITH", "ARMALDO", "FEEBAS", "MILOTIC", "CASTFORM", "KECLEON", "SHUPPET", "BANETTE", "DUSKULL", "DUSCLOPS", "TROPIUS", "CHIMECHO", "ABSOL", "WYNAUT", "SNORUNT", "GLALIE", "SPHEAL", "SEALEO", "WALREIN", "CLAMPERL", "HUNTAIL", "GOREBYSS", "RELICANTH", "LUVDISC", "BAGON", "SHELGON", "SALAMENCE", "BELDUM", "METANG", "METAGROSS", "REGIROCK", "REGICE", "REGISTEEL", "LATIAS", "LATIOS", "KYOGRE", "GROUDON", "RAYQUAZA", "JIRACHI", "DEOXYS", "TURTWIG", "GROTLE", "TORTERRA", "CHIMCHAR", "MONFERNO", "INFERNAPE", "PIPLUP", "PRINPLUP", "EMPOLEON", "STARLY", "STARAVIA", "STARAPTOR", "BIDOOF", "BIBAREL", "KRICKETOT", "KRICKETUNE", "SHINX", "LUXIO", "LUXRAY", "BUDEW", "ROSERADE", "CRANIDOS", "RAMPARDOS", "SHIELDON", "BASTIODON", "BURMY", "WORMADAM", "MOTHIM", "COMBEE", "VESPIQUEN", "PACHIRISU", "BUIZEL", "FLOATZEL", "CHERUBI", "CHERRIM", "SHELLOS", "GASTRODON", "AMBIPOM", "DRIFLOON", "DRIFBLIM", "BUNEARY", "LOPUNNY", "MISMAGIUS", "HONCHKROW", "GLAMEOW", "PURUGLY", "CHINGLING", "STUNKY", "SKUNTANK", "BRONZOR", "BRONZONG", "BONSLY", "MIME JR.", "HAPPINY", "CHATOT", "SPIRITOMB", "GIBLE", "GABITE", "GARCHOMP", "MUNCHLAX", "RIOLU", "LUCARIO", "HIPPOPOTAS", "HIPPOWDON", "SKORUPI", "DRAPION", "CROAGUNK", "TOXICROAK", "CARNIVINE", "FINNEON", "LUMINEON", "MANTYKE", "SNOVER", "ABOMASNOW", "WEAVILE", "MAGNEZONE", "LICKILICKY", "RHYPERIOR", "TANGROWTH", "ELECTIVIRE", "MAGMORTAR", "TOGEKISS", "YANMEGA", "LEAFEON", "GLACEON", "GLISCOR", "MAMOSWINE", "PORYGON-Z", "GALLADE", "PROBOPASS", "DUSKNOIR", "FROSLASS", "ROTOM", "UXIE", "MESPRIT", "AZELF", "DIALGA", "PALKIA", "HEATRAN", "REGIGIGAS", "GIRATINA", "CRESSELIA", "PHIONE", "MANAPHY", "DARKRAI", "SHAYMIN", "ARCEUS" };
        static UInt32[] Growth = { 0x00, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x04, 0x04, 0x00, 0x00, 0x04, 0x04, 0x00, 0x00, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x05, 0x05, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x05, 0x05, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x04, 0x04, 0x04, 0x00, 0x05, 0x05, 0x00, 0x04, 0x04, 0x04, 0x04, 0x00, 0x00, 0x03, 0x03, 0x03, 0x03, 0x04, 0x04, 0x00, 0x03, 0x03, 0x03, 0x03, 0x04, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x04, 0x04, 0x00, 0x00, 0x03, 0x05, 0x03, 0x00, 0x00, 0x00, 0x00, 0x05, 0x05, 0x04, 0x00, 0x00, 0x04, 0x05, 0x05, 0x05, 0x05, 0x00, 0x00, 0x00, 0x00, 0x05, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x04, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x00, 0x00, 0x05, 0x05, 0x05, 0x00, 0x00, 0x02, 0x02, 0x05, 0x05, 0x05, 0x01, 0x01, 0x01, 0x03, 0x03, 0x03, 0x02, 0x02, 0x04, 0x00, 0x04, 0x04, 0x03, 0x04, 0x05, 0x05, 0x05, 0x00, 0x00, 0x05, 0x05, 0x00, 0x00, 0x01, 0x02, 0x03, 0x02, 0x02, 0x05, 0x05, 0x02, 0x02, 0x00, 0x00, 0x00, 0x04, 0x04, 0x04, 0x03, 0x03, 0x03, 0x03, 0x03, 0x01, 0x01, 0x01, 0x02, 0x04, 0x04, 0x00, 0x00, 0x02, 0x02, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x03, 0x04, 0x04, 0x04, 0x04, 0x05, 0x04, 0x03, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x01, 0x01, 0x01, 0x05, 0x04, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x02, 0x02, 0x00, 0x00, 0x04, 0x03, 0x04, 0x04, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x03, 0x00, 0x05, 0x05, 0x05, 0x05, 0x03, 0x03, 0x05, 0x05, 0x05, 0x05, 0x00, 0x00, 0x05, 0x01, 0x01, 0x05, 0x05, 0x05, 0x03, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x03, 0x05, 0x00, 0x05, 0x00, 0x04, 0x00, 0x00, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x05, 0x03, 0x05 };
        static UInt32[] PP = { 0x00, 0x23, 0x19, 0x0A, 0x0F, 0x14, 0x14, 0x0F, 0x0F, 0x0F, 0x23, 0x1E, 0x05, 0x0A, 0x1E, 0x1E, 0x23, 0x23, 0x14, 0x0F, 0x14, 0x14, 0x0F, 0x14, 0x1E, 0x05, 0x19, 0x0F, 0x0F, 0x0F, 0x19, 0x14, 0x05, 0x23, 0x0F, 0x14, 0x14, 0x14, 0x0F, 0x1E, 0x23, 0x14, 0x14, 0x1E, 0x19, 0x28, 0x14, 0x0F, 0x14, 0x14, 0x14, 0x1E, 0x19, 0x0F, 0x1E, 0x19, 0x05, 0x0F, 0x0A, 0x05, 0x14, 0x14, 0x14, 0x05, 0x23, 0x14, 0x19, 0x14, 0x14, 0x14, 0x0F, 0x19, 0x0F, 0x0A, 0x28, 0x19, 0x0A, 0x23, 0x1E, 0x0F, 0x14, 0x28, 0x0A, 0x0F, 0x1E, 0x0F, 0x14, 0x0A, 0x0F, 0x0A, 0x05, 0x0A, 0x0A, 0x19, 0x0A, 0x14, 0x28, 0x1E, 0x1E, 0x14, 0x14, 0x0F, 0x0A, 0x28, 0x0F, 0x0A, 0x1E, 0x14, 0x14, 0x0A, 0x28, 0x28, 0x1E, 0x1E, 0x1E, 0x14, 0x1E, 0x0A, 0x0A, 0x14, 0x05, 0x0A, 0x1E, 0x14, 0x14, 0x14, 0x05, 0x0F, 0x0A, 0x14, 0x0F, 0x0F, 0x23, 0x14, 0x0F, 0x0A, 0x14, 0x1E, 0x0F, 0x28, 0x14, 0x0F, 0x0A, 0x05, 0x0A, 0x1E, 0x0A, 0x0F, 0x14, 0x0F, 0x28, 0x28, 0x0A, 0x05, 0x0F, 0x0A, 0x0A, 0x0A, 0x0F, 0x1E, 0x1E, 0x0A, 0x0A, 0x14, 0x0A, 0x01, 0x01, 0x0A, 0x0A, 0x0A, 0x05, 0x0F, 0x19, 0x0F, 0x0A, 0x0F, 0x1E, 0x05, 0x28, 0x0F, 0x0A, 0x19, 0x0A, 0x1E, 0x0A, 0x14, 0x0A, 0x0A, 0x0A, 0x0A, 0x0A, 0x14, 0x05, 0x28, 0x05, 0x05, 0x0F, 0x05, 0x0A, 0x05, 0x0F, 0x0A, 0x0A, 0x0A, 0x14, 0x14, 0x28, 0x0F, 0x0A, 0x14, 0x14, 0x19, 0x05, 0x0F, 0x0A, 0x05, 0x14, 0x0F, 0x14, 0x19, 0x14, 0x05, 0x1E, 0x05, 0x0A, 0x14, 0x28, 0x05, 0x14, 0x28, 0x14, 0x0F, 0x23, 0x0A, 0x05, 0x05, 0x05, 0x0F, 0x05, 0x14, 0x05, 0x05, 0x0F, 0x14, 0x0A, 0x05, 0x05, 0x0F, 0x0F, 0x0F, 0x0F, 0x0A, 0x0A, 0x0A, 0x14, 0x0A, 0x0A, 0x0A, 0x0A, 0x0F, 0x0F, 0x0F, 0x0A, 0x14, 0x14, 0x0A, 0x14, 0x14, 0x14, 0x14, 0x14, 0x0A, 0x0A, 0x0A, 0x14, 0x14, 0x05, 0x0F, 0x0A, 0x0A, 0x0F, 0x0A, 0x14, 0x05, 0x05, 0x0A, 0x0A, 0x14, 0x05, 0x0A, 0x14, 0x0A, 0x14, 0x14, 0x14, 0x05, 0x05, 0x0F, 0x14, 0x0A, 0x0F, 0x14, 0x0F, 0x0A, 0x0A, 0x0F, 0x0A, 0x05, 0x05, 0x0A, 0x0F, 0x0A, 0x05, 0x14, 0x19, 0x05, 0x28, 0x0A, 0x05, 0x28, 0x0F, 0x14, 0x14, 0x05, 0x0F, 0x14, 0x1E, 0x0F, 0x0F, 0x05, 0x0A, 0x1E, 0x14, 0x1E, 0x0F, 0x05, 0x28, 0x0F, 0x05, 0x14, 0x05, 0x0F, 0x19, 0x28, 0x0F, 0x14, 0x0F, 0x14, 0x0F, 0x14, 0x0A, 0x14, 0x14, 0x05, 0x05, 0x0A, 0x05, 0x28, 0x0A, 0x0A, 0x05, 0x0A, 0x0A, 0x0F, 0x0A, 0x14, 0x1E, 0x1E, 0x0A, 0x14, 0x05, 0x0A, 0x0A, 0x0F, 0x0A, 0x0A, 0x05, 0x0F, 0x05, 0x0A, 0x0A, 0x1E, 0x14, 0x14, 0x0A, 0x0A, 0x05, 0x05, 0x0A, 0x05, 0x14, 0x0A, 0x14, 0x0A, 0x0F, 0x0A, 0x14, 0x14, 0x14, 0x0F, 0x0F, 0x0A, 0x0F, 0x14, 0x0F, 0x0A, 0x0A, 0x0A, 0x14, 0x05, 0x1E, 0x05, 0x0A, 0x0F, 0x0A, 0x0A, 0x05, 0x14, 0x1E, 0x0A, 0x1E, 0x0F, 0x0F, 0x0F, 0x0F, 0x1E, 0x0A, 0x14, 0x0F, 0x0A, 0x0A, 0x14, 0x0F, 0x05, 0x05, 0x0F, 0x0F, 0x05, 0x0A, 0x05, 0x14, 0x05, 0x0F, 0x14, 0x05, 0x14, 0x14, 0x14, 0x14, 0x0A, 0x14, 0x0A, 0x0F, 0x14, 0x0F, 0x0A, 0x0A, 0x05, 0x0A, 0x05, 0x05, 0x0A, 0x05, 0x05, 0x0A, 0x05, 0x05, 0x05 };

        #region ExpTable
        //From PKHEX
        internal static readonly uint[,] ExpTable =
       {
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0},
            {8, 15, 4, 9, 6, 10},
            {27, 52, 13, 57, 21, 33},
            {64, 122, 32, 96, 51, 80},
            {125, 237, 65, 135, 100, 156},
            {216, 406, 112, 179, 172, 270},
            {343, 637, 178, 236, 274, 428},
            {512, 942, 276, 314, 409, 640},
            {729, 1326, 393, 419, 583, 911},
            {1000, 1800, 540, 560, 800, 1250},
            {1331, 2369, 745, 742, 1064, 1663},
            {1728, 3041, 967, 973, 1382, 2160},
            {2197, 3822, 1230, 1261, 1757, 2746},
            {2744, 4719, 1591, 1612, 2195, 3430},
            {3375, 5737, 1957, 2035, 2700, 4218},
            {4096, 6881, 2457, 2535, 3276, 5120},
            {4913, 8155, 3046, 3120, 3930, 6141},
            {5832, 9564, 3732, 3798, 4665, 7290},
            {6859, 11111, 4526, 4575, 5487, 8573},
            {8000, 12800, 5440, 5460, 6400, 10000},
            {9261, 14632, 6482, 6458, 7408, 11576},
            {10648, 16610, 7666, 7577, 8518, 13310},
            {12167, 18737, 9003, 8825, 9733, 15208},
            {13824, 21012, 10506, 10208, 11059, 17280},
            {15625, 23437, 12187, 11735, 12500, 19531},
            {17576, 26012, 14060, 13411, 14060, 21970},
            {19683, 28737, 16140, 15244, 15746, 24603},
            {21952, 31610, 18439, 17242, 17561, 27440},
            {24389, 34632, 20974, 19411, 19511, 30486},
            {27000, 37800, 23760, 21760, 21600, 33750},
            {29791, 41111, 26811, 24294, 23832, 37238},
            {32768, 44564, 30146, 27021, 26214, 40960},
            {35937, 48155, 33780, 29949, 28749, 44921},
            {39304, 51881, 37731, 33084, 31443, 49130},
            {42875, 55737, 42017, 36435, 34300, 53593},
            {46656, 59719, 46656, 40007, 37324, 58320},
            {50653, 63822, 50653, 43808, 40522, 63316},
            {54872, 68041, 55969, 47846, 43897, 68590},
            {59319, 72369, 60505, 52127, 47455, 74148},
            {64000, 76800, 66560, 56660, 51200, 80000},
            {68921, 81326, 71677, 61450, 55136, 86151},
            {74088, 85942, 78533, 66505, 59270, 92610},
            {79507, 90637, 84277, 71833, 63605, 99383},
            {85184, 95406, 91998, 77440, 68147, 106480},
            {91125, 100237, 98415, 83335, 72900, 113906},
            {97336, 105122, 107069, 89523, 77868, 121670},
            {103823, 110052, 114205, 96012, 83058, 129778},
            {110592, 115015, 123863, 102810, 88473, 138240},
            {117649, 120001, 131766, 109923, 94119, 147061},
            {125000, 125000, 142500, 117360, 100000, 156250},
            {132651, 131324, 151222, 125126, 106120, 165813},
            {140608, 137795, 163105, 133229, 112486, 175760},
            {148877, 144410, 172697, 141677, 119101, 186096},
            {157464, 151165, 185807, 150476, 125971, 196830},
            {166375, 158056, 196322, 159635, 133100, 207968},
            {175616, 165079, 210739, 169159, 140492, 219520},
            {185193, 172229, 222231, 179056, 148154, 231491},
            {195112, 179503, 238036, 189334, 156089, 243890},
            {205379, 186894, 250562, 199999, 164303, 256723},
            {216000, 194400, 267840, 211060, 172800, 270000},
            {226981, 202013, 281456, 222522, 181584, 283726},
            {238328, 209728, 300293, 234393, 190662, 297910},
            {250047, 217540, 315059, 246681, 200037, 312558},
            {262144, 225443, 335544, 259392, 209715, 327680},
            {274625, 233431, 351520, 272535, 219700, 343281},
            {287496, 241496, 373744, 286115, 229996, 359370},
            {300763, 249633, 390991, 300140, 240610, 375953},
            {314432, 257834, 415050, 314618, 251545, 393040},
            {328509, 267406, 433631, 329555, 262807, 410636},
            {343000, 276458, 459620, 344960, 274400, 428750},
            {357911, 286328, 479600, 360838, 286328, 447388},
            {373248, 296358, 507617, 377197, 298598, 466560},
            {389017, 305767, 529063, 394045, 311213, 486271},
            {405224, 316074, 559209, 411388, 324179, 506530},
            {421875, 326531, 582187, 429235, 337500, 527343},
            {438976, 336255, 614566, 447591, 351180, 548720},
            {456533, 346965, 639146, 466464, 365226, 570666},
            {474552, 357812, 673863, 485862, 379641, 593190},
            {493039, 367807, 700115, 505791, 394431, 616298},
            {512000, 378880, 737280, 526260, 409600, 640000},
            {531441, 390077, 765275, 547274, 425152, 664301},
            {551368, 400293, 804997, 568841, 441094, 689210},
            {571787, 411686, 834809, 590969, 457429, 714733},
            {592704, 423190, 877201, 613664, 474163, 740880},
            {614125, 433572, 908905, 636935, 491300, 767656},
            {636056, 445239, 954084, 660787, 508844, 795070},
            {658503, 457001, 987754, 685228, 526802, 823128},
            {681472, 467489, 1035837, 710266, 545177, 851840},
            {704969, 479378, 1071552, 735907, 563975, 881211},
            {729000, 491346, 1122660, 762160, 583200, 911250},
            {753571, 501878, 1160499, 789030, 602856, 941963},
            {778688, 513934, 1214753, 816525, 622950, 973360},
            {804357, 526049, 1254796, 844653, 643485, 1005446},
            {830584, 536557, 1312322, 873420, 664467, 1038230},
            {857375, 548720, 1354652, 902835, 685900, 1071718},
            {884736, 560922, 1415577, 932903, 707788, 1105920},
            {912673, 571333, 1460276, 963632, 730138, 1140841},
            {941192, 583539, 1524731, 995030, 752953, 1176490},
            {970299, 591882, 1571884, 1027103, 776239, 1212873},
            {1000000, 600000, 1640000, 1059860, 800000, 1250000},
        };
        #endregion

        //From PKHEX
        public static readonly byte[][] blockPosition =
        {
            new byte[] {0, 0, 0, 0, 0, 0, 1, 1, 2, 3, 2, 3, 1, 1, 2, 3, 2, 3, 1, 1, 2, 3, 2, 3},
            new byte[] {1, 1, 2, 3, 2, 3, 0, 0, 0, 0, 0, 0, 2, 3, 1, 1, 3, 2, 2, 3, 1, 1, 3, 2},
            new byte[] {2, 3, 1, 1, 3, 2, 2, 3, 1, 1, 3, 2, 0, 0, 0, 0, 0, 0, 3, 2, 3, 2, 1, 1},
            new byte[] {3, 2, 3, 2, 1, 1, 3, 2, 3, 2, 1, 1, 3, 2, 3, 2, 1, 1, 0, 0, 0, 0, 0, 0},
        };

        //From PKHEX
        public static readonly byte[] blockPositionInvert =
        {
            0, 1, 2, 4, 3, 5, 6, 7, 12, 18, 13, 19, 8, 10, 14, 20, 16, 22, 9, 11, 15, 21, 17, 23
        };

        static TcpClient tcpClient = new TcpClient();
        static String host = "127.0.0.1";
        static Int32 port = 6000;
        static String file = "teams.json";
        static Byte[] Reply;
        static UInt32 TeamPointer = 0;
        static String[] ResultArray;
        static UInt32 ResultValue;
        static Random Rand = new Random();
        static String JSONString;
        static JavaScriptSerializer ser;
        static Player player;

        static void Main(string[] args)
        {
            for(int i = 0; i < args.Length; i+=2)
            {
                if(args[i] == "-ip")
                {
                    host = args[i + 1];
                }

                if (args[i] == "-p")
                {
                    port = Convert.ToInt32(args[i + 1]);
                }

                if (args[i] == "-f")
                {
                    file = args[i + 1];
                }
            }

            if (tcpClient.ConnectAsync(host, port).Wait(1000))
            {
                //Pointer@0x918F4FFC
                do
                {
                    SendCommandAndWait("READ 32 2442088444\n", 0x918F4FFC);
                }
                while (ResultValue == 0);

                TeamPointer = ResultValue;
                if (TeamPointer != 0x00)
                {
                    JSONString = File.ReadAllText(file);
                    ser = new JavaScriptSerializer();
                    player = ser.Deserialize<Player>(JSONString);
                    InjectTeam(player.player1, TeamPointer + 0x5AB74);
                    InjectTeam(player.player2, TeamPointer + 0x5B94C);
                }
            }
        }

        static void ReadMemory(UInt32 offset, Byte[] buffer, Int32 size)
        {
            for (int i = 0; i < size; i += 4)
            {
                SendCommandAndWait("READ 32 " + (offset + (uint)i).ToString() + "\n", offset + (uint)i);
                if (ResultValue == 0x00)//if 0 try again maybe it was error
                {
                    SendCommandAndWait("READ 32 " + (offset + (uint)i).ToString() + "\n", offset + (uint)i);
                }
                buffer[i] = (byte)((ResultValue >> 0x18) & 0xFF);
                buffer[i + 1] = (byte)((ResultValue >> 0x10) & 0xFF);
                buffer[i + 2] = (byte)((ResultValue >> 0x08) & 0xFF);
                buffer[i + 3] = (byte)(ResultValue & 0xFF);
            }
        }

        static void WriteMemory(UInt32 offset, Byte[] buffer, Int32 size)
        {
            UInt32 Value;
            for (int i = 0; i < size; i += 4)
            {
                Value = (uint)(buffer[i+3] + (buffer[i+2] << 0x08) + (buffer[i+1] << 0x10) + (buffer[i] << 0x18));
                SendCommand("WRITE 32 " + (offset + i).ToString() + " " + Value.ToString() + "\n");
                SendCommand("WRITE 32 " + (offset + i).ToString() + " " + Value.ToString() + "\n");//Just in case of error
            }
        }

        //From PKHEX
        static byte[] shuffleArray45(byte[] data, uint sv)
        {
            byte[] sdata = new byte[data.Length];
            Array.Copy(data, sdata, 8); // Copy unshuffled bytes

            // Shuffle Away!
            for (int block = 0; block < 4; block++)
                Array.Copy(data, 8 + 32 * blockPosition[block][sv], sdata, 8 + 32 * block, 32);

            return sdata;
        }

        static UInt32 GeneratePID(Byte Nature, Boolean Shiny, UInt32 TIDSID)
        {
            UInt32 PIDl = (uint)Rand.Next(0x10000);
            UInt32 PIDh = (uint)Rand.Next(0x10000);

            if (Shiny)
            {
                while (((PIDl ^ PIDh ^ TIDSID) >= 8) || (((PIDl + (PIDh << 0x10)) % 25) != Nature))
                {
                    PIDl = (uint)Rand.Next(0x10000);
                    PIDh = TIDSID ^ PIDl;
                }

            }
            else
            {
                while (((PIDl ^ PIDh ^ TIDSID) < 8) || (((PIDl + (PIDh << 0x10)) % 25) != Nature))
                {
                    PIDl = (uint)Rand.Next(0x10000);
                    PIDh = (uint)Rand.Next(0x10000);
                }
            }
            return PIDl + (PIDh << 0x10);
        }

        static Byte GetGender(String Gender)
        {
            Byte result = 0;
            switch (Gender)
            {
                case "m":
                    {
                        result =  0;
                    }
                    break;

                case "f":
                    {
                        result =  1;
                    }
                    break;

                case "-":
                case null:
                    {
                        result = 2;
                    }
                    break;
            }
            return result;
        }

        static UInt32 GetIVs(Stats IVs)
        {
            return ((uint)IVs.spd << 2) + ((uint)IVs.spa << 7) + ((uint)IVs.spe << 12) + ((uint)IVs.def << 17) + ((uint)IVs.atk << 22) + ((uint)IVs.hp << 27);
        }

        static void InjectTeam(Pokemon[] pokemon, UInt32 TeamOffset)
        {
            Byte[] PokemonData = new Byte[0x88];
            Byte[] PokemonData2 = new Byte[0x88];
            Byte[] NameData = new Byte[0x16];
            UInt16 CharValue;
            UInt32 TID;
            UInt32 SID;
            UInt32 pv;
            UInt32 sv;

            for (int i = 0; i < pokemon.Length; i++)
            {
                ReadMemory(TeamOffset + ((uint)i * 0x8C), PokemonData, PokemonData.Length);
                pv = PokemonData[0x03] + ((uint)PokemonData[0x02] << 0x08) + ((uint)PokemonData[0x01] << 0x10) + ((uint)PokemonData[0x00] << 0x18);
                sv = ((pv & 0x3E000) >> 0xD) % 24;
                PokemonData = shuffleArray45(PokemonData, sv);
                SID = (ushort)(PokemonData[0x0D] + (PokemonData[0x0C] << 0x08));
                TID = (ushort)(PokemonData[0x0F] + (PokemonData[0x0E] << 0x08));


                //PID
                pv = GeneratePID(pokemon[i].nature.id, pokemon[i].species.shiny, (uint)(TID ^ SID));
                sv = ((pv & 0x3E000) >> 0xD) % 24;
                PokemonData[0x00] = (byte)(pv >> 0x18);
                PokemonData[0x01] = (byte)((pv >> 0x10) & 0xFF);
                PokemonData[0x02] = (byte)((pv >> 0x08) & 0xFF);
                PokemonData[0x03] = (byte)(pv & 0xFF);

                //Pokemon
                PokemonData[0x08] = (byte)(pokemon[i].species.id >> 0x08);
                PokemonData[0x09] = (byte)(pokemon[i].species.id & 0xFF);

                //Item
                PokemonData[0x0A] = (byte)(pokemon[i].item.id >> 0x08);
                PokemonData[0x0B] = (byte)(pokemon[i].item.id & 0xFF);

                //Experience
                PokemonData[0x10] = (byte)(ExpTable[50, Growth[pokemon[i].species.id]] >> 0x18);
                PokemonData[0x11] = (byte)((ExpTable[50, Growth[pokemon[i].species.id]] >> 0x10) & 0xFF);
                PokemonData[0x12] = (byte)((ExpTable[50, Growth[pokemon[i].species.id]] >> 0x08) & 0xFF);
                PokemonData[0x13] = (byte)(ExpTable[50, Growth[pokemon[i].species.id]] & 0xFF);

                //Ability
                PokemonData[0x15] = pokemon[i].ability.id;

                //EVs
                PokemonData[0x18] = pokemon[i].evs.hp;
                PokemonData[0x19] = pokemon[i].evs.atk;
                PokemonData[0x1A] = pokemon[i].evs.def;
                PokemonData[0x1B] = pokemon[i].evs.spe;
                PokemonData[0x1C] = pokemon[i].evs.spa;
                PokemonData[0x1D] = pokemon[i].evs.spd;

                //Moves
                PokemonData[0x28] = (byte)(pokemon[i].moves[0].id >> 0x08);
                PokemonData[0x29] = (byte)(pokemon[i].moves[0].id & 0xFF);
                PokemonData[0x2A] = (byte)(pokemon[i].moves[1].id >> 0x08);
                PokemonData[0x2B] = (byte)(pokemon[i].moves[1].id & 0xFF);
                PokemonData[0x2C] = (byte)(pokemon[i].moves[2].id >> 0x08);
                PokemonData[0x2D] = (byte)(pokemon[i].moves[2].id & 0xFF);
                PokemonData[0x2E] = (byte)(pokemon[i].moves[3].id >> 0x08);
                PokemonData[0x2F] = (byte)(pokemon[i].moves[3].id & 0xFF);

                //PPs
                PokemonData[0x30] = (byte)(PP[pokemon[i].moves[0].id] * 1.6);
                PokemonData[0x31] = (byte)(PP[pokemon[i].moves[1].id] * 1.6);
                PokemonData[0x32] = (byte)(PP[pokemon[i].moves[2].id] * 1.6);
                PokemonData[0x33] = (byte)(PP[pokemon[i].moves[3].id] * 1.6);

                //PPUps
                PokemonData[0x34] = 0x03;
                PokemonData[0x35] = 0x03;
                PokemonData[0x36] = 0x03;
                PokemonData[0x37] = 0x03;

                //IVs
                PokemonData[0x38] = (byte)(GetIVs(pokemon[i].ivs) >> 0x18);
                PokemonData[0x39] = (byte)((GetIVs(pokemon[i].ivs) >> 0x10) & 0xFF);
                PokemonData[0x3A] = (byte)((GetIVs(pokemon[i].ivs) >> 0x08) & 0xFF);
                PokemonData[0x3B] = (byte)(GetIVs(pokemon[i].ivs) & 0xFF);

                //Gender/Form
                PokemonData[0x40] = (byte)(pokemon[i].species.form + ((GetGender(pokemon[i].species.gender) & 0x03) << 0x05));

                //Nickname
                for (int j = 0; j < 0x16; j += 2)
                {
                    if ((j / 2) < Names[pokemon[i].species.id].Length)
                    {
                        CharValue = BitConverter.ToUInt16(System.Text.Encoding.Unicode.GetBytes(Names[pokemon[i].species.id][j / 2].ToString()), 0);
                        switch (CharValue)
                        {
                            case 0x20://Space
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0xDE;
                                }
                                break;

                            case 0x27://'
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0xB3;
                                }
                                break;

                            case 0x2D://-
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0xBE;
                                }
                                break;

                            case 0x2E://.
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0xAE;
                                }
                                break;

                            case 0x32://2
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0x23;
                                }
                                break;

                            case 0x2640://♀
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0xBC;
                                }
                                break;

                            case 0x2642://♂
                                {
                                    PokemonData[0x48 + j] = 0x01;
                                    PokemonData[0x48 + j + 1] = 0xBB;
                                }
                                break;

                            default:
                                {
                                    PokemonData[0x48 + j] = (byte)((CharValue + 0xEA) >> 0x08);
                                    PokemonData[0x48 + j + 1] = (byte)((CharValue + 0xEA) & 0xFF);
                                }
                                break;
                        }
                    }
                    else
                    {
                        PokemonData[0x48 + j] = 0xFF;
                        PokemonData[0x48 + j + 1] = 0xFF;
                    }

                }

                PokemonData = shuffleArray45(PokemonData, blockPositionInvert[sv]);
                WriteMemory(TeamOffset + ((uint)i * 0x8C), PokemonData, PokemonData.Length);
            }
        }

        static void SendCommand(String Command)
        {
            if (tcpClient.Connected)
            {
                Byte[] Response = Encoding.UTF8.GetBytes(Command);
                tcpClient.GetStream().Write(Response, 0, Response.Length);
            }

        }

        static void SendCommandAndWait(String Command, UInt32 Offset)
        {
            if (tcpClient.Connected)
            {
                Byte[] Response = Encoding.UTF8.GetBytes(Command);
                tcpClient.GetStream().Write(Response, 0, Response.Length);

                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
                Reply = new Byte[tcpClient.Available];
                tcpClient.GetStream().Read(Reply, 0, Reply.Length);
                ResultArray = Encoding.UTF8.GetString(Reply).Split(' ');
                ResultValue = Convert.ToUInt32(ResultArray[2].Substring(0, ResultArray[2].Length - 1));
            }
        }
    }
}