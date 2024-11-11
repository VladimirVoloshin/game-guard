import React from 'react';
import { CheckCircleOutlined, MinusCircleOutlined } from '@ant-design/icons';
import './Styles.css'

interface ReviewStatusElemProps {
  isReviewed: boolean;
}

const ReviewStatusElem: React.FC<ReviewStatusElemProps> = ({ isReviewed }) => {
  return (
    <div className={`review-status ${isReviewed ? 'reviewed' : 'not-reviewed'}`}>
      {isReviewed ? (
        <CheckCircleOutlined className="review-icon" />
      ) : (
        <MinusCircleOutlined className="review-icon" />
      )}
    </div>
  );
};

export default ReviewStatusElem;