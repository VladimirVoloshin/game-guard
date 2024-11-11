import { format } from 'date-fns';

export const formatDateTime = (date: Date): string => {
    return format(date, process.env.REACT_APP_DATE_FORMAT ?? 'dd-MM-yyyy HH:mm:ss.SSS');
  }