import React from 'react';
import { Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';
import type { MenuProps } from 'antd';
import './Styles.css';  

const { Header, Content, Footer } = Layout;

interface LayoutProps {
  children: React.ReactNode;
}

const AppLayout: React.FC<LayoutProps> = ({ children }) => {
  const menuItems: MenuProps['items'] = [
    {
      key: '1',
      label: <Link to="/">Dashboard</Link>,
    },
    {
      key: '2',
      label: <Link to="/player-management">Player Management</Link>,
    },
  ];

  return (
    <Layout className="layout">
      <Header>
        <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['1']} items={menuItems} />
      </Header>
      <Content>
        <div className="site-layout-content">{children}</div>
      </Content>
      <Footer>Game Bot Detection Dashboard ©2024</Footer>
    </Layout>
  );
}

export default AppLayout;