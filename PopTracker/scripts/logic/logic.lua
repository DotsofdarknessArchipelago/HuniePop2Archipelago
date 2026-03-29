-- put logic functions here using the Lua API: https://github.com/black-sliver/PopTracker/blob/master/doc/PACKS.md#lua-interface
-- don't be afraid to use custom logic functions. it will make many things a lot easier to maintain, for example by adding logging.
-- to see how this function gets called, check: locations/locations.json
-- example:
function has_more_then_n_consumable(n)
    local count = Tracker:ProviderCountForCode('consumable')
    local val = (count > tonumber(n))
    if ENABLE_DEBUG_LOG then
        print(string.format("called has_more_then_n_consumable: count: %s, n: %s, val: %s", count, n, val))
    end
    if val then
        return 1 -- 1 => access is in logic
    end
    return 0 -- 0 => no access
end

function girl_unique_gift_id(girl, giftslot, giftid)
    local count = Tracker:ProviderCountForCode(girl..' unique'..giftslot)
    local val = (count == tonumber(giftid))
    if val then
        return 1 -- 1 => access is in logic
    end
    return 0 -- 0 => no access
end

function girl_shoe_gift_id(girl, giftslot, giftid)
    local count = Tracker:ProviderCountForCode(girl..' shoe'..giftslot)
    local val = (count == tonumber(giftid))
    if val then
        return 1 -- 1 => access is in logic
    end
    return 0 -- 0 => no access
end

function girl_unlocked(girl)
    if Tracker:ProviderCountForCode("unlockgirl("..girl..")") >0 then
        if (girl == "abia") then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(candace)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|candace)") >0)
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(jessie)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|jessie)") >0)
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(lillian)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|lillian)") >0)
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(lola)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|lola)") >0)
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif (girl == "ashley") then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(brooke)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|brooke)") >0)
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(lillian)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|lillian)") >0)
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(nora)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|nora)") >0)
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(polly)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|polly)") >0)
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "brooke" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(ashley)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|brooke)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(jessie)") >0 and Tracker:ProviderCountForCode("pairunlock(brooke|jessie)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(polly)") >0 and Tracker:ProviderCountForCode("pairunlock(brooke|polly)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(sarah)") >0 and Tracker:ProviderCountForCode("pairunlock(brooke|sarah)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "candace" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(abia)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|candace)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(lailani)") >0 and Tracker:ProviderCountForCode("pairunlock(candace|lailani)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(nora)") >0 and Tracker:ProviderCountForCode("pairunlock(candace|nora)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(polly)") >0 and Tracker:ProviderCountForCode("pairunlock(candace|polly)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "jessie" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(abia)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|jessie)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(brooke)") >0 and Tracker:ProviderCountForCode("pairunlock(brooke|jessie)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(lailani)") >0 and Tracker:ProviderCountForCode("pairunlock(jessie|lailani)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(lola)") >0 and Tracker:ProviderCountForCode("pairunlock(jessie|lola)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "lailani" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(candace)") >0 and Tracker:ProviderCountForCode("pairunlock(candace|lailani)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(jessie)") >0 and Tracker:ProviderCountForCode("pairunlock(jessie|lailani)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(lillian)") >0 and Tracker:ProviderCountForCode("pairunlock(lailani|lillian)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(sarah)") >0 and Tracker:ProviderCountForCode("pairunlock(lailani|sarah)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "lillian" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(abia)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|lillian)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(ashley)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|lillian)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(lailani)") >0 and Tracker:ProviderCountForCode("pairunlock(lailani|lillian)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(zoey)") >0 and Tracker:ProviderCountForCode("pairunlock(lillian|zoey)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "lola" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(abia)") >0 and Tracker:ProviderCountForCode("pairunlock(abia|lola)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(jessie)") >0 and Tracker:ProviderCountForCode("pairunlock(jessie|lola)") >0)
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(nora)") >0 and Tracker:ProviderCountForCode("pairunlock(lola|nora)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(zoey)") >0 and Tracker:ProviderCountForCode("pairunlock(lola|zoey)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "nora" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(ashley)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|nora)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(candace)") >0 and Tracker:ProviderCountForCode("pairunlock(candace|nora)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(lola)") >0 and Tracker:ProviderCountForCode("pairunlock(lola|nora)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(sarah)") >0 and Tracker:ProviderCountForCode("pairunlock(nora|sarah)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "polly" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(ashley)") >0 and Tracker:ProviderCountForCode("pairunlock(ashley|polly)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(brooke)") >0 and Tracker:ProviderCountForCode("pairunlock(brooke|polly)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(candace)") >0 and Tracker:ProviderCountForCode("pairunlock(candace|polly)") >0)
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(zoey)") >0 and Tracker:ProviderCountForCode("pairunlock(polly|zoey)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "sarah" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(brooke)") >0 and Tracker:ProviderCountForCode("pairunlock(brooke|sarah)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(lailani)") >0 and Tracker:ProviderCountForCode("pairunlock(lailani|sarah)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(nora)") >0 and Tracker:ProviderCountForCode("pairunlock(nora|sarah)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(zoey)") >0 and Tracker:ProviderCountForCode("pairunlock(sarah|zoey)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        elseif girl == "zoey" then
            local p1 = (Tracker:ProviderCountForCode("unlockgirl(lillian)") >0 and Tracker:ProviderCountForCode("pairunlock(lillian|zoey)") >0) 
            local p2 = (Tracker:ProviderCountForCode("unlockgirl(lola)") >0 and Tracker:ProviderCountForCode("pairunlock(lola|zoey)") >0) 
            local p3 = (Tracker:ProviderCountForCode("unlockgirl(polly)") >0 and Tracker:ProviderCountForCode("pairunlock(polly|zoey)") >0) 
            local p4 = (Tracker:ProviderCountForCode("unlockgirl(sarah)") >0 and Tracker:ProviderCountForCode("pairunlock(sarah|zoey)") >0) 
            
            if p1 or p2 or p3 or p4 then
                return 1
            end
        end
    end
    return 0
end

function pair_unlocked(girl1, girl2)
    local p = (Tracker:ProviderCountForCode("unlockgirl("..girl1..")") >0 and Tracker:ProviderCountForCode("unlockgirl("..girl2..")") >0 and Tracker:ProviderCountForCode("pairunlock("..girl1.."|"..girl2..")") >0)
    if p then
        return 1
    end
    return 0
end