import ActivityLog from "../../components/ActivityLog/ActivityLog";
import PlayersSummary from "../../components/PlayersSummary/PlayersSummary";

const DashboardPage: React.FC = () => {
  return (
    <div>
      <PlayersSummary />
      <ActivityLog />
    </div>
  );
};

export default DashboardPage;