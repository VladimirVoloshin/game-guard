import { notification } from "antd";

export const withErrorHandling = async <T>(
  apiCall: () => Promise<T>,
  errorMessage: string
): Promise<T | null> => {
  try {
    return await apiCall();
  } catch (error) {
    console.error(errorMessage, error);
    notification.error({
      message: 'Error',
      description: errorMessage,
    });
    return null;
  }
};