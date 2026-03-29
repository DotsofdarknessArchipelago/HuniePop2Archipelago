-- entry point for all lua code of the pack
-- more info on the lua API: https://github.com/black-sliver/PopTracker/blob/master/doc/PACKS.md#lua-interface
ENABLE_DEBUG_LOG = true
-- get current variant
local variant = Tracker.ActiveVariantUID
-- check variant info
IS_ITEMS_ONLY = variant:find("itemsonly")

print("-- Example Tracker --")
print("Loaded variant: ", variant)
if ENABLE_DEBUG_LOG then
    print("Debug logging is enabled!")
end

-- Utility Script for helper functions etc.
ScriptHost:LoadScript("scripts/utils.lua")

-- Logic
ScriptHost:LoadScript("scripts/logic/logic.lua")

-- Custom Items
ScriptHost:LoadScript("scripts/custom_items/class.lua")
ScriptHost:LoadScript("scripts/custom_items/progressiveTogglePlus.lua")
ScriptHost:LoadScript("scripts/custom_items/progressiveTogglePlusWrapper.lua")

-- Items
Tracker:AddItems("items/items.jsonc")

if not IS_ITEMS_ONLY then -- <--- use variant info to optimize loading
    -- Maps
    Tracker:AddMaps("maps/maps.jsonc")
    -- Locations
    --Tracker:AddLocations("locations/abia.json")
    Tracker:AddLocations("locations/gifts/abia.json")
    Tracker:AddLocations("locations/outfits/abia.json")
    Tracker:AddLocations("locations/pairs/abia.json")
    Tracker:AddLocations("locations/questions/abia.json")

    --Tracker:AddLocations("locations/ashley.json")
    Tracker:AddLocations("locations/gifts/ashley.json")
    Tracker:AddLocations("locations/outfits/ashley.json")
    Tracker:AddLocations("locations/pairs/ashley.json")
    Tracker:AddLocations("locations/questions/ashley.json")

    --Tracker:AddLocations("locations/brooke.json")
    Tracker:AddLocations("locations/gifts/brooke.json")
    Tracker:AddLocations("locations/outfits/brooke.json")
    Tracker:AddLocations("locations/pairs/brooke.json")
    Tracker:AddLocations("locations/questions/brooke.json")

    --Tracker:AddLocations("locations/candace.json")
    Tracker:AddLocations("locations/gifts/candace.json")
    Tracker:AddLocations("locations/outfits/candace.json")
    Tracker:AddLocations("locations/pairs/candace.json")
    Tracker:AddLocations("locations/questions/candace.json")

    --Tracker:AddLocations("locations/jessie.json")
    Tracker:AddLocations("locations/gifts/jessie.json")
    Tracker:AddLocations("locations/outfits/jessie.json")
    Tracker:AddLocations("locations/pairs/jessie.json")
    Tracker:AddLocations("locations/questions/jessie.json")

    --Tracker:AddLocations("locations/lailani.json")
    Tracker:AddLocations("locations/gifts/lailani.json")
    Tracker:AddLocations("locations/outfits/lailani.json")
    Tracker:AddLocations("locations/pairs/lailani.json")
    Tracker:AddLocations("locations/questions/lailani.json")

    --Tracker:AddLocations("locations/lillian.json")
    Tracker:AddLocations("locations/gifts/lillian.json")
    Tracker:AddLocations("locations/outfits/lillian.json")
    Tracker:AddLocations("locations/pairs/lillian.json")
    Tracker:AddLocations("locations/questions/lillian.json")

    --Tracker:AddLocations("locations/lola.json")
    Tracker:AddLocations("locations/gifts/lola.json")
    Tracker:AddLocations("locations/outfits/lola.json")
    Tracker:AddLocations("locations/pairs/lola.json")
    Tracker:AddLocations("locations/questions/lola.json")

    --Tracker:AddLocations("locations/nora.json")
    Tracker:AddLocations("locations/gifts/nora.json")
    Tracker:AddLocations("locations/outfits/nora.json")
    Tracker:AddLocations("locations/pairs/nora.json")
    Tracker:AddLocations("locations/questions/nora.json")

    --Tracker:AddLocations("locations/polly.json")
    Tracker:AddLocations("locations/gifts/polly.json")
    Tracker:AddLocations("locations/outfits/polly.json")
    Tracker:AddLocations("locations/pairs/polly.json")
    Tracker:AddLocations("locations/questions/polly.json")

    --Tracker:AddLocations("locations/sarah.json")
    Tracker:AddLocations("locations/gifts/sarah.json")
    Tracker:AddLocations("locations/outfits/sarah.json")
    Tracker:AddLocations("locations/pairs/sarah.json")
    Tracker:AddLocations("locations/questions/sarah.json")

    --Tracker:AddLocations("locations/zoey.json")
    Tracker:AddLocations("locations/gifts/zoey.json")
    Tracker:AddLocations("locations/outfits/zoey.json")
    Tracker:AddLocations("locations/pairs/zoey.json")
    Tracker:AddLocations("locations/questions/zoey.json")
end

-- Layout
Tracker:AddLayouts("layouts/items.jsonc")
Tracker:AddLayouts("layouts/pairs.json")
Tracker:AddLayouts("layouts/girls.json")
Tracker:AddLayouts("layouts/unique.json")
Tracker:AddLayouts("layouts/shoes.json")
Tracker:AddLayouts("layouts/wings.json")
Tracker:AddLayouts("layouts/outfit.json")
Tracker:AddLayouts("layouts/misc.json")
Tracker:AddLayouts("layouts/tracker.jsonc")
Tracker:AddLayouts("layouts/broadcast.jsonc")

-- AutoTracking for Poptracker
if PopVersion and PopVersion >= "0.18.0" then
    ScriptHost:LoadScript("scripts/autotracking.lua")
end
