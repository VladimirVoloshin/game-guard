import React, { useState, useEffect } from 'react';
import { Row, Col, Card, Statistic } from 'antd';
import { UserOutlined, ExclamationCircleOutlined, StopOutlined } from '@ant-design/icons';
import { PlayersSummaryModel } from '../../models/Players/PlayersSummaryModel';
import { getPlayersSummaryAsync } from '../../services/PlayersClient';
import './Styles.css';

const PlayersSummary: React.FC = () => {
    const [stats, setStats] = useState<PlayersSummaryModel>();

    useEffect(() => {
        fetchPlayersSummary();
    }, []);

    const fetchPlayersSummary = async () => {   
        const response = await getPlayersSummaryAsync()
        setStats(response);   
    };

    return (
        <>
            <h2>Players Summary</h2>
            <Row gutter={16}>
                <Col span={6}>
                    <Card>
                        <Statistic
                            title="Total Players"
                            value={stats?.total}
                            prefix={<UserOutlined />}
                        />
                    </Card>
                </Col>
                <Col span={6}>
                    <Card>
                        <Statistic
                            title="Active Players"
                            value={stats?.active}
                            className="active-players"
                        />
                    </Card>
                </Col>
                <Col span={6}>
                    <Card>
                        <Statistic
                            title="Suspicious Players"
                            value={stats?.suspicious}
                            className="suspicious-players"
                            prefix={<ExclamationCircleOutlined />}
                        />
                    </Card>
                </Col>
                <Col span={6}>
                    <Card>
                        <Statistic
                            title="Banned Players"
                            value={stats?.banned}
                            className="banned-players"
                            prefix={<StopOutlined />}
                        />
                    </Card>
                </Col>
            </Row>
        </>
    );
};

export default PlayersSummary;