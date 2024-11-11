import React, { useState, useEffect } from 'react';
import { Table, Button, Switch, Select, Pagination } from 'antd';
import { CheckCircleOutlined, MinusCircleOutlined, ReloadOutlined } from '@ant-design/icons';
import './Styles.css';
import { getPlayersAsync } from '../../services/PlayersClient';
import { getActivityLogs, reviewActivityLog } from '../../services/ActivityLogClient';
import { formatDateTime } from '../../utils/DateUtils';
import { PlayerModel } from '../../models/Players/PlayerModel';
import { ActivityLogModel } from '../../models/ActivityLogs/ActivityLogModel';

const { Option } = Select;

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
  }, [autoRefresh, selectedPlayers, isSuspicious, currentPage]);

  const fetchActivities = async () => {
    const result = await getActivityLogs(selectedPlayers, isSuspicious, currentPage, pageSize);
    setActivities(result.items);
    setTotalCount(result.totalCount);
  };

  const fetchPlayers = async () => {
    const fetchedPlayers = await getPlayersAsync();
    setPlayers(fetchedPlayers);
  };

  const columns = [
    { title: 'Player', dataIndex: 'playerUsername', key: 'playerUsername' },
    { title: 'Action', dataIndex: 'action', key: 'action' },
    {
      title: 'Timestamp',
      dataIndex: 'timestamp',
      key: 'timestamp',
      render: (timestamp: string) => formatDateTime(new Date(timestamp))
    },
    {
      title: 'Suspicious',
      dataIndex: 'isSuspicious',
      key: 'isSuspicious',
      render: (isSuspicious: boolean, record: ActivityLogModel) => (
        <div
          className="suspicious-switch-container"
          onClick={() => handleReviewActivityLog(record, !isSuspicious)}
        >
          <Switch
            checked={isSuspicious}
            className={isSuspicious ? 'suspicious-switch-on' : 'suspicious-switch-off'}
          />
          <span className="suspicious-label">
            {isSuspicious ? 'yes' : 'no'}
          </span>
        </div>
      ),
    },
    {
      title: 'Reviewed',
      dataIndex: 'isReviewed',
      key:'isReviewed',
      render: (isReviewed: boolean) => (
        <div className={`review-status ${isReviewed ? 'reviewed' : 'not-reviewed'}`}>
          {isReviewed ? (
            <CheckCircleOutlined className="review-icon" />
          ) : (
            <MinusCircleOutlined className="review-icon" />
          )}
        </div>
      ),
    }
  ];

  const handleReviewActivityLog = async (record: ActivityLogModel, checked: boolean) => {
    await reviewActivityLog(record.id, !record.isSuspicious);
    setActivities(activities.map(activity =>
      activity.id === record.id ? { ...activity, isSuspicious: checked, isReviewed: true } : activity
    ));
  }; 

  return (
    <div>
      <div className="activity-log-header">
        <h2>Activity Log</h2>
        <div className="activity-log-controls">
          <Select
            mode="multiple"
            className="player-select"
            placeholder="Select players"
            onChange={(values: number[]) => setSelectedPlayers(values)}
            value={selectedPlayers}
          >
            {players.map(player => (
              <Option key={player.id} value={player.id}>{player.username}</Option>
            ))}
          </Select>
          <Select
            className="suspicious-select"
            placeholder="Suspicious"
            onChange={(value: number) => setIsSuspicious(value)}
            value={isSuspicious}
          >
            <Option value={-1}>All</Option>
            <Option value={1}>Suspicious</Option>
            <Option value={0}>Not Suspicious</Option>
          </Select>
          <Button
            icon={<ReloadOutlined className="reload-icon" />} 
            onClick={fetchActivities}
            className="refresh-button"
          >
            Refresh
          </Button>
          <div className="auto-refresh-container">
            <Switch
              checked={autoRefresh}
              onChange={setAutoRefresh}
              className="auto-refresh-switch"
            />
            <span className="auto-refresh-label">auto</span>
          </div>
        </div>
      </div>
      <Table 
        dataSource={activities} 
        columns={columns} 
        rowClassName={(record) => record.isSuspicious ? 'suspicious-row' : ''}
        pagination={false}
        rowKey={(record) => record.id}
      />
      <Pagination
        current={currentPage}
        total={totalCount}
        pageSize={pageSize}
        onChange={(page) => setCurrentPage(page)}
        className="activity-log-pagination"
      />    
    </div>
  );
}

export default ActivityLog;