import React from 'react';
import { Pagination  } from 'antd';
import './Styles.css'

interface CustPaginationProps {
  currentPage: number;
  setCurrentPage: (page: number) => void;
  pageSize: number;
  setPageSize: (size: number) => void;
  totalCount: number;
}

const CustPagination: React.FC<CustPaginationProps> = ({
  currentPage,
  setCurrentPage,
  pageSize,
  setPageSize,
  totalCount
}) => {
  const handleShowSizeChange = (current: number, size: number) => {
    setPageSize(size);
    setCurrentPage(1);
  };

  return (
    <Pagination
      current={currentPage}
      total={totalCount}
      pageSize={pageSize}
      onChange={setCurrentPage}
      className="pagination"
      onShowSizeChange={handleShowSizeChange}
      showSizeChanger
    />
  );
};

export default CustPagination;