import React from 'react';
import { Select } from 'antd';
import { PlayerStatus } from '../../../models/Players/PlayerStatus';
import './Styles.css'

const { Option } = Select;

interface PlayerStatusSelectProps {
    statusId: number;
    playerId: number;
    onStatusChange: (playerId: number, newStatusId: number) => void;
}

const PlayerStatusSelect: React.FC<PlayerStatusSelectProps> = ({ statusId, playerId, onStatusChange }) => {
    return (
        <Select
            defaultValue={statusId}
            className="status-select"
            onChange={(value: number) => onStatusChange(playerId, value)}
        >
            <Option value={PlayerStatus.Active}>Active</Option>
            <Option value={PlayerStatus.Suspicious}>Suspicious</Option>
            <Option value={PlayerStatus.Banned}>Banned</Option>
        </Select>
    );
}

export default PlayerStatusSelect;