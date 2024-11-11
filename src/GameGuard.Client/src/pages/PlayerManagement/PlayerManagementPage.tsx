


import React, { useState, useEffect } from 'react';
import { PlayerModel } from '../../models/Players/PlayerModel';
import './Styles.css';
import { getPlayersAsync, updatePlayerStatusAsync } from '../../services/PlayersClient';
import PlayerTable from '../../components/PlayerTable/PlayerTable';

const PlayerManagementPage: React.FC = () => {
  const [players, setPlayers] = useState<PlayerModel[]>([]);

  useEffect(() => {
    fetchPlayers();
  }, []);

  const fetchPlayers = async () => {
    const response = await getPlayersAsync()
    if (response)
      setPlayers(response);
  };

  const handleStatusChange = async (playerId: number, newStatusId: number) => {    
    await updatePlayerStatusAsync(playerId, newStatusId);    
  };

  return (
    <div className="player-management">
      <h1>Player Management</h1>
      <PlayerTable players={players} onStatusChange={handleStatusChange} />
    </div>
  );
}

export default PlayerManagementPage;