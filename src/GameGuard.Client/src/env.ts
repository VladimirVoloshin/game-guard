import dotenv from 'dotenv';

dotenv.config();

interface ENV {
    GAME_GUARD_API_URL: string | undefined;
}

interface Config {
    GAME_GUARD_API_URL: string;
}

const getConfig = (): ENV => {
    return {
        GAME_GUARD_API_URL: process.env.GAME_GUARD_API_URL
    };
};

const getSanitzedConfig = (config: ENV): Config => {
    for (const [key, value] of Object.entries(config)) {
        if (value === undefined) {
            throw new Error(`Missing key ${key} in config.env`);
        }
    }
    return config as Config;
};

const rawConfig = getConfig();

const appConfig = getSanitzedConfig(rawConfig);

export default appConfig;