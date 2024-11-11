import React from 'react';
import { Button, Switch, Select } from 'antd';
import { ReloadOutlined } from '@ant-design/icons';
import { PlayerModel } from '../../../models/Players/PlayerModel';
import './Styles.css'

const { Option } = Select;

interface ActivityLogHeaderProps {
  players: PlayerModel[];
  selectedPlayers: number[];
  setSelectedPlayers: (players: number[]) => void;
  isSuspicious: number;
  setIsSuspicious: (value: number) => void;
  autoRefresh: boolean;
  setAutoRefresh: (value: boolean) => void;
  fetchActivities: () => void;
}

const ActivityLogHeader: React.FC<ActivityLogHeaderProps> = ({
  players,
  selectedPlayers,
  setSelectedPlayers,
  isSuspicious,
  setIsSuspicious,
  autoRefresh,
  setAutoRefresh,
  fetchActivities
}) => {
  return (
    <div className="activity-log-header">
      <h2>Activity Log</h2>
      <div className="activity-log-controls">
        <Select
          mode="multiple"
          className="player-select"
          placeholder="Select players"
          onChange={setSelectedPlayers}
          value={selectedPlayers}
        >
          {players.map(player => (
            <Option key={player.id} value={player.id}>{player.username}</Option>
          ))}
        </Select>
        <Select
          className="suspicious-select"
          placeholder="Suspicious"
          onChange={setIsSuspicious}
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
  );
};

export default ActivityLogHeader;