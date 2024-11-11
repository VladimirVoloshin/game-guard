import { PlayerModel } from "../models/Players/PlayerModel";
import { PlayersSummaryModel } from "../models/Players/PlayersSummaryModel";
import requests from "./AxiousClient";


export const getPlayersSummaryAsync = async (): Promise<PlayersSummaryModel> => {
    return await requests.get<PlayersSummaryModel>(
        `/api/players/summary`
    );
};

export const getPlayersAsync = async () => {
    return await requests.get<PlayerModel[]>(
        `/api/players`
    );
};
