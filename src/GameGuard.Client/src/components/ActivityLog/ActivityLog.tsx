import React, { useState, useEffect } from 'react';
import './Styles.css';
import { getPlayersAsync } from '../../services/PlayersClient';
import { getActivityLogs, reviewActivityLog } from '../../services/ActivityLogClient';
import { PlayerModel } from '../../models/Players/PlayerModel';
import { ActivityLogModel } from '../../models/ActivityLogs/ActivityLogModel';
import CustPagination from '../Common/Paggination/Paggination';
import ActivityLogTable from './ActivityLogTable/ActivityLogTable';
import ActivityLogHeader from './ActivityLogHeader/ActivityLogHeader';


const AUTO_REFRESH_SECONDS = 10;
const PAGE_SIZE = 10;

const ActivityLog: React.FC = () => {
  const [activities, setActivities] = useState<ActivityLogModel[]>([]);
  const [autoRefresh, setAutoRefresh] = useState<boolean>(false);
  const [selectedPlayers, setSelectedPlayers] = useState<number[]>([]);
  const [isSuspicious, setIsSuspicious] = useState<number>(-1);
  const [pageSize, setPageSize] = useState<number>(PAGE_SIZE);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalCount, setTotalCount] = useState<number>(0);
  const [players, setPlayers] = useState<PlayerModel[]>([]);

  useEffect(() => {
    fetchActivities();
    fetchPlayers();

    if (autoRefresh) {
      const interval = setInterval(fetchActivities, AUTO_REFRESH_SECONDS * 1000);
      return () => clearInterval(interval);
    }
  }, [autoRefresh, selectedPlayers, isSuspicious, currentPage, pageSize]);

  const fetchActivities = async () => {
    const result = await getActivityLogs(selectedPlayers, isSuspicious, currentPage, pageSize);
    setActivities(result.items);
    setTotalCount(result.totalCount);
  };

  const fetchPlayers = async () => {
    const fetchedPlayers = await getPlayersAsync();
    setPlayers(fetchedPlayers);
  };  

  const handleReviewActivityLog = async (record: ActivityLogModel, checked: boolean) => {
    await reviewActivityLog(record.id, !record.isSuspicious);
    setActivities(activities.map(activity =>
      activity.id === record.id ? { ...activity, isSuspicious: checked, isReviewed: true } : activity
    ));
  }; 

  return (
    <div>
       <ActivityLogHeader
        players={players}
        selectedPlayers={selectedPlayers}
        setSelectedPlayers={setSelectedPlayers}
        isSuspicious={isSuspicious}
        setIsSuspicious={setIsSuspicious}
        autoRefresh={autoRefresh}
        setAutoRefresh={setAutoRefresh}
        fetchActivities={fetchActivities}
      />
      <ActivityLogTable
        activities={activities}
        handleReviewActivityLog={handleReviewActivityLog}
      />
      <CustPagination
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        pageSize={pageSize}
        setPageSize={setPageSize}
        totalCount={totalCount}
      />    
    </div>
  );
}

export default ActivityLog;