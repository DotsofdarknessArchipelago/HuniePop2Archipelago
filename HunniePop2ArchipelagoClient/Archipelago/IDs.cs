

using HunniePop2ArchipelagoClient.HuniePop2.Gameplay;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements.Experimental;

namespace HunniePop2ArchipelagoClient.Archipelago
{
    public class IDs
    {

        public static int shoeoffsettoid(int offset)
        {
            return offset switch
            {
                1 => 189,//Knitted Boots
                2 => 190,//Seasonal Boots
                3 => 191,//Heavy Boots
                4 => 192,//Fuzzy Boots
                5 => 193,//Festive Boots
                6 => 194,//Elegant Peep Toes
                7 => 195,//Angel Peep Toes
                8 => 196,//Plaid Peep Toes
                9 => 197,//Ribbon Peep Toes
                10 => 198,//Leopard Peep Toes
                11 => 199,//Suede Booties
                12 => 200,//Striped Booties
                13 => 201,//Goth Booties
                14 => 202,//Abstract Booties
                15 => 203,//Satanic Booties
                16 => 204,//Astro Boots
                17 => 205,//Sacred Boots
                18 => 206,//Sherbet Boots
                19 => 207,//Hydro Boots
                20 => 208,//Cosmic Boots
                21 => 209,//Geta Platforms
                22 => 210,//Candy Platforms
                23 => 211,//Light Up Platforms
                24 => 212,//Rainbow Platforms
                25 => 213,//Star Platforms
                26 => 214,//Comfy Flip Flops
                27 => 215,//Palm Flip Flops
                28 => 216,//Melon Flip Flops
                29 => 217,//Aqua Flip Flops
                30 => 218,//Garden Flip Flops
                31 => 219,//Glittery Heels
                32 => 220,//Golden Heels
                33 => 221,//Studded Heels
                34 => 222,//Neon Heels
                35 => 223,//Clear Heels
                36 => 224,//Skater Sneakers
                37 => 225,//Ballin' Sneakers
                38 => 226,//High Top Sneakers
                39 => 227,//Airy Sneakers
                40 => 228,//Training Sneakers
                41 => 229,//Patriotic Wedges
                42 => 230,//Charcoal Wedges
                43 => 231,//Cork Wedges
                44 => 232,//Wooden Wedges
                45 => 233,//Denim Wedges
                46 => 234,//Zip Up Gladiators
                47 => 235,//Strap Gladiators
                48 => 236,//Weave Gladiators
                49 => 237,//Modest Gladiators
                50 => 238,//Web Gladiators
                51 => 239,//Crafted Flats
                52 => 240,//Basic Flats
                53 => 241,//Floral Flats
                54 => 242,//Open Flats
                55 => 243,//Cozy Flats
                56 => 244,//Girly Pumps
                57 => 245,//Classy Pumps
                58 => 246,//Shiny Pumps
                59 => 247,//Polka Dot Pumps
                60 => 248,//Fancy Pumps
                _ => -1,
            };
        }
        
        public static int shoeidtooffset(int id)
        {
            return id switch
            {
                189 => 1 ,//Knitted Boots
                190 => 2 ,//Seasonal Boots
                191 => 3 ,//Heavy Boots
                192 => 4 ,//Fuzzy Boots
                193 => 5 ,//Festive Boots
                194 => 6 ,//Elegant Peep Toes
                195 => 7 ,//Angel Peep Toes
                196 => 8 ,//Plaid Peep Toes
                197 => 9 ,//Ribbon Peep Toes
                198 => 10 ,//Leopard Peep Toes
                199 => 11 ,//Suede Booties
                200 => 12 ,//Striped Booties
                201 => 13 ,//Goth Booties
                202 => 14 ,//Abstract Booties
                203 => 15 ,//Satanic Booties
                204 => 16 ,//Astro Boots
                205 => 17 ,//Sacred Boots
                206 => 18 ,//Sherbet Boots
                207 => 19 ,//Hydro Boots
                208 => 20 ,//Cosmic Boots
                209 => 21 ,//Geta Platforms
                210 => 22 ,//Candy Platforms
                211 => 23 ,//Light Up Platforms
                212 => 24 ,//Rainbow Platforms
                213 => 25 ,//Star Platforms
                214 => 26 ,//Comfy Flip Flops
                215 => 27 ,//Palm Flip Flops
                216 => 28 ,//Melon Flip Flops
                217 => 29 ,//Aqua Flip Flops
                218 => 30 ,//Garden Flip Flops
                219 => 31 ,//Glittery Heels
                220 => 32 ,//Golden Heels
                221 => 33 ,//Studded Heels
                222 => 34 ,//Neon Heels
                223 => 35 ,//Clear Heels
                224 => 36 ,//Skater Sneakers
                225 => 37 ,//Ballin' Sneakers
                226 => 38 ,//High Top Sneakers
                227 => 39 ,//Airy Sneakers
                228 => 40 ,//Training Sneakers
                229 => 41 ,//Patriotic Wedges
                230 => 42 ,//Charcoal Wedges
                231 => 43 ,//Cork Wedges
                232 => 44 ,//Wooden Wedges
                233 => 45 ,//Denim Wedges
                234 => 46 ,//Zip Up Gladiators
                235 => 47 ,//Strap Gladiators
                236 => 48 ,//Weave Gladiators
                237 => 49 ,//Modest Gladiators
                238 => 50 ,//Web Gladiators
                239 => 51 ,//Crafted Flats
                240 => 52 ,//Basic Flats
                241 => 53 ,//Floral Flats
                242 => 54 ,//Open Flats
                243 => 55 ,//Cozy Flats
                244 => 56 ,//Girly Pumps
                245 => 57 ,//Classy Pumps
                246 => 58 ,//Shiny Pumps
                247 => 59 ,//Polka Dot Pumps
                248 => 60,//Fancy Pumps
                _ => -1,
            };
        }

        public static int uniqueoffsettoid(int offset)
        {
            return offset switch
            {
                1 => 129,//Scissors
                2 => 130,//Spool Of Thread
                3 => 131,//Buttons
                4 => 132,//Pincushion
                5 => 133,//Measuring Tape
                6 => 134,//Gin
                7 => 135,//Rum
                8 => 136,//Whisky
                9 => 137,//Vodka
                10 => 138,//Tequila
                11 => 139,//Witch Hat
                12 => 140,//Jack O Lantern
                13 => 141,//Ouija Board
                14 => 142,//Voodoo Doll
                15 => 143,//Goat Skull
                16 => 144,//Crystals
                17 => 145,//Crystal Ball
                18 => 146,//Incense
                19 => 147,//Hourglass
                20 => 148,//Tarot Cards
                21 => 149,//Manga Book
                22 => 150,//Booby Mousepad
                23 => 151,//Cellphone Cover
                24 => 152,//Chibi Figurine
                25 => 153,//Japanese Candy
                26 => 154,//Bath Salts
                27 => 155,//Hot Stones
                28 => 156,//Loofah Sponge
                29 => 157,//Warm Towels
                30 => 158,//Cucumber Slices
                31 => 159,//Letter Blocks
                32 => 160,//Ring Stacker
                33 => 161,//Mini Xylophone
                34 => 162,//Shapes Block
                35 => 163,//Animal Wheel
                36 => 164,//Binky
                37 => 165,//Baby Cap
                38 => 166,//Diapers
                39 => 167,//Baby Bottle
                40 => 168,//Car Carrier
                41 => 169,//Clutch Purse
                42 => 170,//Shoulder Bag
                43 => 171,//Quilted Handbag
                44 => 172,//Fancy Handbag
                45 => 173,//Elegant Tote Bag
                46 => 174,//Microphone
                47 => 175,//Bass Guitar
                48 => 176,//Drum Kit
                49 => 177,//Drum Sticks
                50 => 178,//Guitar Amp
                51 => 179,//Spanking Paddle
                52 => 180,//Ball Gag
                53 => 181,//Nipple Clamps
                54 => 182,//Handcuffs
                55 => 183,//Chain Collar
                56 => 184,//Television
                57 => 185,//Jukebox
                58 => 186,//Phonograph
                59 => 187,//Radio
                60 => 188,//Soda Machine
                _ => -1,
            };
        }
        
        public static int uniqueidtooffset(int id)
        {
            return id switch
            {
                129 => 1 ,//Scissors
                130 => 2 ,//Spool Of Thread
                131 => 3 ,//Buttons
                132 => 4 ,//Pincushion
                133 => 5 ,//Measuring Tape
                134 => 6 ,//Gin
                135 => 7 ,//Rum
                136 => 8 ,//Whisky
                137 => 9 ,//Vodka
                138 => 10 ,//Tequila
                139 => 11 ,//Witch Hat
                140 => 12 ,//Jack O Lantern
                141 => 13 ,//Ouija Board
                142 => 14 ,//Voodoo Doll
                143 => 15 ,//Goat Skull
                144 => 16 ,//Crystals
                145 => 17 ,//Crystal Ball
                146 => 18 ,//Incense
                147 => 19 ,//Hourglass
                148 => 20 ,//Tarot Cards
                149 => 21 ,//Manga Book
                150 => 22 ,//Booby Mousepad
                151 => 23 ,//Cellphone Cover
                152 => 24 ,//Chibi Figurine
                153 => 25 ,//Japanese Candy
                154 => 26 ,//Bath Salts
                155 => 27 ,//Hot Stones
                156 => 28 ,//Loofah Sponge
                157 => 29 ,//Warm Towels
                158 => 30 ,//Cucumber Slices
                159 => 31 ,//Letter Blocks
                160 => 32 ,//Ring Stacker
                161 => 33 ,//Mini Xylophone
                162 => 34 ,//Shapes Block
                163 => 35 ,//Animal Wheel
                164 => 36 ,//Binky
                165 => 37 ,//Baby Cap
                166 => 38 ,//Diapers
                167 => 39 ,//Baby Bottle
                168 => 40 ,//Car Carrier
                169 => 41 ,//Clutch Purse
                170 => 42 ,//Shoulder Bag
                171 => 43 ,//Quilted Handbag
                172 => 44 ,//Fancy Handbag
                173 => 45 ,//Elegant Tote Bag
                174 => 46 ,//Microphone
                175 => 47 ,//Bass Guitar
                176 => 48 ,//Drum Kit
                177 => 49 ,//Drum Sticks
                178 => 50 ,//Guitar Amp
                179 => 51 ,//Spanking Paddle
                180 => 52 ,//Ball Gag
                181 => 53 ,//Nipple Clamps
                182 => 54 ,//Handcuffs
                183 => 55 ,//Chain Collar
                184 => 56 ,//Television
                185 => 57 ,//Jukebox
                186 => 58 ,//Phonograph
                187 => 59 ,//Radio
                188 => 60,//Soda Machine
                _ => -1,
            };
        }
        
        public static int filleroffsettoid(int id)
        {
            return id switch
            {
                1 => 250,//Ocean Breeze Candle
                2 => 251,//Pine Forest Candle
                3 => 252,//Pumpkin Spice Candle
                4 => 253,//Cinnamon Cider Candle
                5 => 254,//Tropical Sunset Candle
                6 => 255,//Midnight Moonlight Candle
                7 => 256,//Sweet Honeycomb Candle
                8 => 257,//Spring Rain Candle
                9 => 258,//Cotton Pillows Candle
                10 =>  259,//Exfoliating Scrub
                11 =>  261,//Eyelash Kit
                12 =>  262,//Powder Brush
                13 =>  263,//Makeup Palette
                14 =>  264,//Lipstick
                15 =>  265,//Moisturizer
                16 =>  266,//Hair Brush
                17 =>  268,//Blow Dryer
                18 =>  25,//Blue Orchid Flowers
                19 =>  26,//Green Clover Flowers
                20 =>  27,//Orange Daisy Flowers
                21 =>  28,//Red Rose Flowers
                22 =>  29,//Pink Cosmos Flowers
                23 =>  30,//Violet Pansy Flowers
                24 =>  32,//Yellow Narcissus Flowers
                25 =>  31,//Turquoise Tulip Flowers
                26 =>  33,//White Lily Flowers
                27 =>  284,//Sanitary Pad
                28 =>  285,//Tampon
                29 =>  286,//Feminine Wash
                30 =>  287,//Feminine Cream
                31 =>  288,//Feminine Wipes
                32 =>  289,//Douche
                33 =>  34,//Sapphire Ring
                34 =>  35,//Emerald Bracelet
                35 =>  36,//Citrine Bracelet
                36 =>  37,//Ruby Ring
                37 =>  38,//Spinel Necklace
                38 =>  39,//Amethyst Necklace
                39 =>  41,//Topaz Earrings
                40 =>  40,//Aquamarine Earrings
                41 =>  42,//Diamond Tiara
                42 =>  43,//Peacock Plush
                43 =>  44,//Frog Plush
                44 =>  45,//Goldfish Plush
                45 =>  46,//Ladybug Plush
                46 =>  47,//Pig Plush
                47 =>  48,//Octopus Plush
                48 =>  50,//Chick Plush
                49 =>  49,//Elephant Plush
                50 =>  51,//Bunny Plush
                51 =>  52,//Fox Plush
                52 =>  249,//Cow Plush
                53 =>  294,//Magic Wand
                54 =>  295,//Egg Vibrator
                55 =>  296,//Butt Plug
                56 =>  297,//Anal Beads
                57 =>  298,//Lube
                58 =>  299,//Dirty Dice
                59 =>  300,//Kamasutra
                60 => 301,//Breast Pump
                61 => 269,//Beach Ball
                62 => 270,//Inner Tube
                63 => 271,//Tiki Head Charm
                64 => 272,//Surfboard
                65 => 273,//Snorkel Mask
                66 => 274,//Flippers
                67 => 275,//Suntan Lotion
                68 => 276,//Beach Towel
                69 => 277,//Tropical Lei
                70 => 278,//Ceramic Fish
                71 => 279,//Pinwheel
                72 => 280,//Hemp Bracelet
                73 => 281,//Glass Dolphin
                74 => 282,//Postcard
                75 => 283,//Snow Globe
                _ => -1,
            };
        }

        public static int baggageoffsettoid(int offset)
        {
            return offset switch
            {
                1 => 93,//Busy Schedule
                2 => 94,//Caffeine Junkie
                3 => 95,//Miss Independent
                4 => 96,//Depression
                5 => 97,//Emphysema
                6 => 98,//Busted Vadge
                7 => 99,//The Darkness
                8 => 100,//Asthma
                9 => 101,//Teen Angst
                10 => 102,//Aquaphobic
                11 => 103,//Tinnitus
                12 => 104,//Kinda Crazy
                13 => 105,//Annoying as Fuck
                14 => 106,//Attention Whore
                15 => 107,//Smelly Pussy
                16 => 108,//Old Fashioned
                17 => 109,//Low Self-Esteem
                18 => 110,//Sheepish
                19 => 111,//Intellectually Challenged
                20 => 112,//Hypersensitive
                21 => 113,//Forgetful
                22 => 114,//Emotionally Guarded
                23 => 115,//Abandonment Issues
                24 => 116,//Vindictive
                25 => 117,//Gold Digger
                26 => 118,//Unsentimental
                27 => 119,//Expensive Tastes
                28 => 120,//Commitment Issues
                29 => 121,//Easily Bored
                30 => 122,//Allergies
                31 => 123,//Self-Effacing
                32 => 124,//One Pump Chump
                33 => 125,//Sex Addict
                34 => 126,//Jealousy
                35 => 127,//Drama Queen
                36 => 128,//Brand Loyalist
                _ => -1,
            };
        }








































        public static int unknownidtoid(int id)
        {
            DepartLocation.gift_unique_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["gift_unique_item_start"]);
            DepartLocation.gift_shoe_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["gift_shoe_item_start"]);
            DepartLocation.lola_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["lola_baggage_item_start"]);

            if (id > DepartLocation.gift_unique_item_start && id <= DepartLocation.gift_shoe_item_start)
            {
                return uniqueoffsettoid((int)(id - DepartLocation.gift_unique_item_start));
            }
            else if (id > DepartLocation.gift_shoe_item_start && id <= DepartLocation.lola_baggage_item_start)
            {
                return shoeoffsettoid((int)(id - DepartLocation.gift_shoe_item_start));
            }
            return -1;
        }




        /// <summary>
        /// HELPER METHOD TO CONVERT AN ITEM ID TO A FLAG/ARCHIPELAGO ID SINCE THERE ARE 5 UNIQUE/SHOE GIFT ITEMS
        /// IN THE GAME BUT YOU CAN ONLY GIVE 4 TO A GIRL 
        /// </summary>
        public static int idtoflag(int id, bool t)
        {
            if (id == 130) { return 69420093; }
            else if (id == 131) { return 69420094; }
            else if (id == 132) { return 69420095; }
            else if (id == 133) { return 69420096; }
            else if (id == 189) { return 69420141; }
            else if (id == 190) { return 69420142; }
            else if (id == 191) { return 69420143; }
            else if (id == 192) { return 69420144; }

            else if (id == 134) { return 69420097; }
            else if (id == 135) { return 69420098; }
            else if (id == 136) { return 69420099; }
            else if (id == 137) { return 69420100; }
            else if (id == 195) { return 69420145; }
            else if (id == 196) { return 69420146; }
            else if (id == 197) { return 69420147; }
            else if (id == 198) { return 69420148; }

            else if (id == 139) { return 69420101; }
            else if (id == 140) { return 69420102; }
            else if (id == 141) { return 69420103; }
            else if (id == 142) { return 69420104; }
            else if (id == 199) { return 69420149; }
            else if (id == 200) { return 69420150; }
            else if (id == 201) { return 69420151; }
            else if (id == 203) { return 69420152; }

            else if (id == 144) { return 69420105; }
            else if (id == 145) { return 69420106; }
            else if (id == 147) { return 69420107; }
            else if (id == 148) { return 69420108; }
            else if (id == 204) { return 69420153; }
            else if (id == 205) { return 69420154; }
            else if (id == 206) { return 69420155; }
            else if (id == 207) { return 69420156; }

            else if (id == 149) { return 69420109; }
            else if (id == 150) { return 69420110; }
            else if (id == 151) { return 69420111; }
            else if (id == 152) { return 69420112; }
            else if (id == 209) { return 69420157; }
            else if (id == 210) { return 69420158; }
            else if (id == 212) { return 69420159; }
            else if (id == 213) { return 69420160; }

            else if (id == 154) { return 69420113; }
            else if (id == 155) { return 69420114; }
            else if (id == 156) { return 69420115; }
            else if (id == 157) { return 69420116; }
            else if (id == 215) { return 69420161; }
            else if (id == 216) { return 69420162; }
            else if (id == 217) { return 69420163; }
            else if (id == 218) { return 69420164; }

            else if (id == 159) { return 69420117; }
            else if (id == 160) { return 69420118; }
            else if (id == 162) { return 69420119; }
            else if (id == 163) { return 69420120; }
            else if (id == 219) { return 69420165; }
            else if (id == 221) { return 69420166; }
            else if (id == 222) { return 69420167; }
            else if (id == 223) { return 69420168; }

            else if (id == 164) { return 69420121; }
            else if (id == 166) { return 69420122; }
            else if (id == 167) { return 69420123; }
            else if (id == 168) { return 69420124; }
            else if (id == 225) { return 69420169; }
            else if (id == 226) { return 69420170; }
            else if (id == 227) { return 69420171; }
            else if (id == 228) { return 69420172; }

            else if (id == 169) { return 69420125; }
            else if (id == 170) { return 69420126; }
            else if (id == 171) { return 69420127; }
            else if (id == 173) { return 69420128; }
            else if (id == 230) { return 69420173; }
            else if (id == 231) { return 69420174; }
            else if (id == 232) { return 69420175; }
            else if (id == 233) { return 69420176; }

            else if (id == 174) { return 69420129; }
            else if (id == 175) { return 69420130; }
            else if (id == 177) { return 69420131; }
            else if (id == 178) { return 69420132; }
            else if (id == 234) { return 69420177; }
            else if (id == 235) { return 69420178; }
            else if (id == 236) { return 69420179; }
            else if (id == 237) { return 69420180; }

            else if (id == 179) { return 69420133; }
            else if (id == 180) { return 69420134; }
            else if (id == 181) { return 69420135; }
            else if (id == 182) { return 69420136; }
            else if (id == 239) { return 69420181; }
            else if (id == 240) { return 69420182; }
            else if (id == 241) { return 69420183; }
            else if (id == 243) { return 69420184; }

            else if (id == 184) { return 69420137; }
            else if (id == 185) { return 69420138; }
            else if (id == 186) { return 69420139; }
            else if (id == 187) { return 69420140; }
            else if (id == 244) { return 69420185; }
            else if (id == 245) { return 69420186; }
            else if (id == 246) { return 69420187; }
            else if (id == 247) { return 69420188; }
            else { return -1; }
        }

        /// <summary>
        /// HELPER METHOD TO CONVERT A FLAG/ARCHIPELAGO ID TO AN ITEM ID SINCE THERE ARE 5 UNIQUE/SHOE GIFT ITEMS
        /// IN THE GAME BUT YOU CAN ONLY GIVE 4 TO A GIRL 
        /// </summary>
        public static int flagtoid(int flag, bool t)
        {
            if (flag == 69420093) { return 130; }
            else if (flag == 69420094) { return 131; }
            else if (flag == 69420095) { return 132; }
            else if (flag == 69420096) { return 133; }
            else if (flag == 69420141) { return 189; }
            else if (flag == 69420142) { return 190; }
            else if (flag == 69420143) { return 191; }
            else if (flag == 69420144) { return 192; }

            else if (flag == 69420097) { return 134; }
            else if (flag == 69420098) { return 135; }
            else if (flag == 69420099) { return 136; }
            else if (flag == 69420100) { return 137; }
            else if (flag == 69420145) { return 195; }
            else if (flag == 69420146) { return 196; }
            else if (flag == 69420147) { return 197; }
            else if (flag == 69420148) { return 198; }

            else if (flag == 69420101) { return 139; }
            else if (flag == 69420102) { return 140; }
            else if (flag == 69420103) { return 141; }
            else if (flag == 69420104) { return 142; }
            else if (flag == 69420149) { return 199; }
            else if (flag == 69420150) { return 200; }
            else if (flag == 69420151) { return 201; }
            else if (flag == 69420152) { return 203; }

            else if (flag == 69420105) { return 144; }
            else if (flag == 69420106) { return 145; }
            else if (flag == 69420107) { return 147; }
            else if (flag == 69420108) { return 148; }
            else if (flag == 69420153) { return 204; }
            else if (flag == 69420154) { return 205; }
            else if (flag == 69420155) { return 206; }
            else if (flag == 69420156) { return 207; }

            else if (flag == 69420109) { return 149; }
            else if (flag == 69420110) { return 150; }
            else if (flag == 69420111) { return 151; }
            else if (flag == 69420112) { return 152; }
            else if (flag == 69420157) { return 209; }
            else if (flag == 69420158) { return 210; }
            else if (flag == 69420159) { return 212; }
            else if (flag == 69420160) { return 213; }

            else if (flag == 69420113) { return 154; }
            else if (flag == 69420114) { return 155; }
            else if (flag == 69420115) { return 156; }
            else if (flag == 69420116) { return 157; }
            else if (flag == 69420161) { return 215; }
            else if (flag == 69420162) { return 216; }
            else if (flag == 69420163) { return 217; }
            else if (flag == 69420164) { return 218; }

            else if (flag == 69420117) { return 159; }
            else if (flag == 69420118) { return 160; }
            else if (flag == 69420119) { return 162; }
            else if (flag == 69420120) { return 163; }
            else if (flag == 69420165) { return 219; }
            else if (flag == 69420166) { return 221; }
            else if (flag == 69420167) { return 222; }
            else if (flag == 69420168) { return 223; }

            else if (flag == 69420121) { return 164; }
            else if (flag == 69420122) { return 166; }
            else if (flag == 69420123) { return 167; }
            else if (flag == 69420124) { return 168; }
            else if (flag == 69420169) { return 225; }
            else if (flag == 69420170) { return 226; }
            else if (flag == 69420171) { return 227; }
            else if (flag == 69420172) { return 228; }

            else if (flag == 69420125) { return 169; }
            else if (flag == 69420126) { return 170; }
            else if (flag == 69420127) { return 171; }
            else if (flag == 69420128) { return 173; }
            else if (flag == 69420173) { return 230; }
            else if (flag == 69420174) { return 231; }
            else if (flag == 69420175) { return 232; }
            else if (flag == 69420176) { return 233; }

            else if (flag == 69420129) { return 174; }
            else if (flag == 69420130) { return 175; }
            else if (flag == 69420131) { return 177; }
            else if (flag == 69420132) { return 178; }
            else if (flag == 69420177) { return 234; }
            else if (flag == 69420178) { return 235; }
            else if (flag == 69420179) { return 236; }
            else if (flag == 69420180) { return 237; }

            else if (flag == 69420133) { return 179; }
            else if (flag == 69420134) { return 180; }
            else if (flag == 69420135) { return 181; }
            else if (flag == 69420136) { return 182; }
            else if (flag == 69420181) { return 239; }
            else if (flag == 69420182) { return 240; }
            else if (flag == 69420183) { return 241; }
            else if (flag == 69420184) { return 243; }

            else if (flag == 69420137) { return 184; }
            else if (flag == 69420138) { return 185; }
            else if (flag == 69420139) { return 186; }
            else if (flag == 69420140) { return 187; }
            else if (flag == 69420185) { return 244; }
            else if (flag == 69420186) { return 245; }
            else if (flag == 69420187) { return 246; }
            else if (flag == 69420188) { return 247; }
            else { return -1; }

        }


        /// <summary>
        /// HELPER METHOD TO CONVERT A ITEM FLAG ID TO AN ITEM ID
        /// </summary>
        public static int itemflagtoid(int flag, bool t)
        {
            if (flag == 69420346) { return 250; }
            else if (flag == 69420347) { return 251; }
            else if (flag == 69420348) { return 252; }
            else if (flag == 69420349) { return 253; }
            else if (flag == 69420350) { return 254; }
            else if (flag == 69420351) { return 255; }
            else if (flag == 69420352) { return 256; }
            else if (flag == 69420353) { return 257; }
            else if (flag == 69420354) { return 258; }
            else if (flag == 69420355) { return 259; }
            else if (flag == 69420356) { return 261; }
            else if (flag == 69420357) { return 262; }
            else if (flag == 69420358) { return 263; }
            else if (flag == 69420359) { return 264; }
            else if (flag == 69420360) { return 265; }
            else if (flag == 69420361) { return 266; }
            else if (flag == 69420362) { return 268; }
            else if (flag == 69420363) { return 25; }
            else if (flag == 69420364) { return 26; }
            else if (flag == 69420365) { return 27; }
            else if (flag == 69420366) { return 28; }
            else if (flag == 69420367) { return 29; }
            else if (flag == 69420368) { return 30; }
            else if (flag == 69420369) { return 32; }
            else if (flag == 69420370) { return 31; }
            else if (flag == 69420371) { return 33; }
            else if (flag == 69420372) { return 284; }
            else if (flag == 69420373) { return 285; }
            else if (flag == 69420374) { return 286; }
            else if (flag == 69420375) { return 287; }
            else if (flag == 69420376) { return 288; }
            else if (flag == 69420377) { return 289; }
            else if (flag == 69420378) { return 34; }
            else if (flag == 69420379) { return 35; }
            else if (flag == 69420380) { return 36; }
            else if (flag == 69420381) { return 37; }
            else if (flag == 69420382) { return 38; }
            else if (flag == 69420383) { return 39; }
            else if (flag == 69420384) { return 41; }
            else if (flag == 69420385) { return 40; }
            else if (flag == 69420386) { return 42; }
            else if (flag == 69420387) { return 43; }
            else if (flag == 69420388) { return 44; }
            else if (flag == 69420389) { return 45; }
            else if (flag == 69420390) { return 46; }
            else if (flag == 69420391) { return 47; }
            else if (flag == 69420392) { return 48; }
            else if (flag == 69420393) { return 50; }
            else if (flag == 69420394) { return 49; }
            else if (flag == 69420395) { return 51; }
            else if (flag == 69420396) { return 52; }
            else if (flag == 69420397) { return 249; }
            else if (flag == 69420398) { return 294; }
            else if (flag == 69420399) { return 295; }
            else if (flag == 69420400) { return 296; }
            else if (flag == 69420401) { return 297; }
            else if (flag == 69420402) { return 298; }
            else if (flag == 69420403) { return 299; }
            else if (flag == 69420404) { return 300; }
            else if (flag == 69420405) { return 301; }
            else if (flag == 69420406) { return 269; }
            else if (flag == 69420407) { return 270; }
            else if (flag == 69420408) { return 271; }
            else if (flag == 69420409) { return 272; }
            else if (flag == 69420410) { return 273; }
            else if (flag == 69420411) { return 274; }
            else if (flag == 69420412) { return 275; }
            else if (flag == 69420413) { return 276; }
            else if (flag == 69420414) { return 277; }
            else if (flag == 69420415) { return 278; }
            else if (flag == 69420416) { return 279; }
            else if (flag == 69420417) { return 280; }
            else if (flag == 69420418) { return 281; }
            else if (flag == 69420419) { return 282; }
            else if (flag == 69420420) { return 283; }

            return 0;
        }

        public static int bagflagtoid(int flag)
        {

            return -1;
        }

    }
}

