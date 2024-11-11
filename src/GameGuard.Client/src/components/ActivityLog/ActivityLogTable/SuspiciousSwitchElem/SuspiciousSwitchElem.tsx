import React from 'react';
import { Switch } from 'antd';
import { ActivityLogModel } from '../../../../models/ActivityLogs/ActivityLogModel';
import './Styles.css'

interface SuspiciousSwitchProps {
  isSuspicious: boolean;
  record: ActivityLogModel;
  handleReviewActivityLog: (record: ActivityLogModel, checked: boolean) => void;
}

const SuspiciousSwitchElem: React.FC<SuspiciousSwitchProps> = ({ isSuspicious, record, handleReviewActivityLog }) => {
  return (
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
  );
};

export default SuspiciousSwitchElem;