import React from 'react';
import { Table } from 'antd';
import { ActivityLogModel } from '../../../models/ActivityLogs/ActivityLogModel';
import { formatDateTime } from '../../../utils/DateUtils';
import SuspiciousSwitchElem from './SuspiciousSwitchElem/SuspiciousSwitchElem';
import ReviewStatusElem from './ReviewStatusElem/ReviewStatusElem';
import './Styles.css'

interface ActivityLogTableProps {
  activities: ActivityLogModel[];
  handleReviewActivityLog: (record: ActivityLogModel, checked: boolean) => void;
}

const ActivityLogTable: React.FC<ActivityLogTableProps> = ({ activities, handleReviewActivityLog }) => {
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
        <SuspiciousSwitchElem
          isSuspicious={isSuspicious}
          record={record}
          handleReviewActivityLog={handleReviewActivityLog}
        />
      ),
    },
    {
      title: 'Reviewed',
      dataIndex: 'isReviewed',
      key:'isReviewed',
      render: (isReviewed: boolean) => <ReviewStatusElem isReviewed={isReviewed} />,
    }
  ];

  return (
    <Table 
      dataSource={activities} 
      columns={columns} 
      rowClassName={(record) => record.isSuspicious ? 'suspicious-row' : ''}
      pagination={false}
      rowKey={(record) => record.id}
    />
  );
};

export default ActivityLogTable;