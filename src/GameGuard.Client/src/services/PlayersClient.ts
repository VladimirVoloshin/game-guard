import { PlayerModel } from "../models/Players/PlayerModel";
import { PlayersSummaryModel } from "../models/Players/PlayersSummaryModel";
import requests from "./AxiousClient";
import { withErrorHandling } from "./errorHandler";

export const getPlayersSummaryAsync = (): Promise<PlayersSummaryModel | null> => {
  return withErrorHandling(
    () => requests.get<PlayersSummaryModel>('/api/players/summary'),
    'Failed to fetch players summary. Please try again later.'
  );
};

export const getPlayersAsync = (): Promise<PlayerModel[] | null> => {
  return withErrorHandling(
    () => requests.get<PlayerModel[]>('/api/players'),
    'Failed to fetch players. Please try again later.'
  );
};

export const updatePlayerStatusAsync = (playerId: number, newStatusId: number): Promise<unknown | null> => {
  return withErrorHandling(
    () => requests.put(`/api/players/${playerId}/status`, { 'statusId': newStatusId }),
    'Failed to update player status. Please try again later.'
  );
};