import { ServerStatus } from '../types/serverStatus';

export const serverStatusToBool = (status: ServerStatus | undefined): boolean | undefined => {
  if (status === undefined) {
    return undefined;
  }
  return status === 'online';
};
