// activityLogClient.ts
import { ActivityLogModel } from "../models/ActivityLogs/ActivityLogModel";
import requests from "./AxiousClient";
import { withErrorHandling } from "./errorHandler";

export const getActivityLogs = (
    playerIds: number[],
    isSuspicious: number,
    page: number,
    pageSize: number
): Promise<{ items: ActivityLogModel[], totalCount: number } | null> => {
    const params = new URLSearchParams();

    playerIds.forEach(id => params.append('PlayerIds', id.toString()));

    if (isSuspicious !== -1) { 
        params.append('IsSuspicious', (isSuspicious === 1).toString()); 
    }
    
    params.append('page', page.toString());
    params.append('pageSize', pageSize.toString());

    return withErrorHandling(
        () => requests.get<{ items: ActivityLogModel[], totalCount: number }>(
            `/api/ActivityLogs?${params.toString()}`
        ),
        'Failed to fetch activity logs. Please try again later.'
    );
};

export const reviewActivityLog = (activityLogId: number, isSuspicious: boolean): Promise<unknown | null> => {
    return withErrorHandling(
        () => requests.put(`/api/ActivityLogs/${activityLogId}/review`, { isSuspicious }),
        'Failed to review activity log. Please try again later.'
    );
};