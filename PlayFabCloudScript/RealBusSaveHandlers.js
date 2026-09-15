// Deploy this file as PlayFab legacy CloudScript. Save payloads are kept in
// internal player data so clients cannot directly overwrite another player.
var SAVE_KEY = "save.player_data.json";

handlers.GetPlayerSaveData = function () {
    var result = server.GetUserInternalData({
        PlayFabId: currentPlayerId,
        Keys: [SAVE_KEY]
    });

    var record = result.Data && result.Data[SAVE_KEY];
    return { payload: record ? record.Value : null };
};

handlers.SetPlayerSaveData = function (args) {
    if (!args || typeof args.payload !== "string" || args.payload.length === 0)
        throw "A non-empty save payload is required.";

    var data = {};
    data[SAVE_KEY] = args.payload;
    server.UpdateUserInternalData({
        PlayFabId: currentPlayerId,
        Data: data
    });
    return { saved: true };
};
