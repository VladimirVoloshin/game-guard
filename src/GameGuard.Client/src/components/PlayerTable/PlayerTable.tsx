import React from 'react';
import { Table } from 'antd';
import { PlayerModel } from '../../models/Players/PlayerModel';
import PlayerStatusSelect from './PlayerStatus/PlayerStatusSelect';

interface PlayerTableProps {
    players: PlayerModel[];
    onStatusChange: (playerId: number, newStatusId: number) => void;
}

const PlayerTable: React.FC<PlayerTableProps> = ({ players, onStatusChange }) => {
    const columns = [
        { title: 'Player ID', dataIndex: 'id', key: 'id' },
        { title: 'Username', dataIndex: 'username', key: 'username' },
        {
            title: 'Status',
            dataIndex: 'statusId',
            key: 'statusId',
            render: (statusId: number, record: PlayerModel) => (
                <PlayerStatusSelect
                    statusId={statusId}
                    playerId={record.id}
                    onStatusChange={onStatusChange}
                />
            ),
        },
    ];

    return (
        <Table
            dataSource={players}
            columns={columns}
            rowKey={(record) => record.id}
        />
    );
}

export default PlayerTable;