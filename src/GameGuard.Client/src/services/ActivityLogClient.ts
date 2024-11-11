import { ActivityLogModel } from "../models/ActivityLogs/ActivityLogModel";
import requests from "./AxiousClient";

export const getActivityLogs = async (
    playerIds: number[],
    isSuspicious: number,
    page: number,
    pageSize: number
): Promise<{ items: ActivityLogModel[], totalCount: number }> => {
    const params = new URLSearchParams();

    playerIds.forEach(id => params.append('PlayerIds', id.toString()));

    if (isSuspicious !== -1) { 
        params.append('IsSuspicious', (isSuspicious === 1).toString()); 
    }
    
    params.append('page', page.toString());
    params.append('pageSize', pageSize.toString());

    return await requests.get<{ items: ActivityLogModel[], totalCount: number }>(
        `/api/ActivityLogs?${params.toString()}`
    );
};

export const reviewActivityLog = async (activityLogId: number, isSuspicious: boolean) => {
    await requests.put(`/api/ActivityLogs/${activityLogId}/review`, { isSuspicious });
};