import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import DashboardPage from './pages/Dashboard/DashboardPage';
import Layout from './Layout/Layout';
import PlayerManagementPage from './pages/PlayerManagement/PlayerManagementPage';

const App: React.FC = () => {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<DashboardPage />} />
          <Route path="/player-management" element={<PlayerManagementPage />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
