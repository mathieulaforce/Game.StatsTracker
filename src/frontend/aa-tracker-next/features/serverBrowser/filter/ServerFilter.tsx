import React, { useContext, useState } from 'react';

export type ServerStatus = 'online' | 'offline';

export interface ServerFilter {
  serverStatus?: 'online' | 'offline';
  hasPlayers?: boolean;
  mapNames: string[];
}

export const initialServerFilterValues: ServerFilter = {
  serverStatus: 'online',
  mapNames: [],
  hasPlayers: undefined,
};

const ServerFilterContext = React.createContext({
  ...initialServerFilterValues,
});

const ServerFilterUpdateContext = React.createContext({
  setServerStatusFilter: (serverStatus?: ServerStatus) => {},
  setPlayerFilter: (hasPlayers: boolean) => {},
  setMapFilter: (mapNames: string[]) => {},
  setFilter: (filter: ServerFilter) => {},
  resetFilter: () => {},
});

export const useServerFilter = () => {
  return useContext(ServerFilterContext);
};
export const useUpdateServerFilter = () => {
  return useContext(ServerFilterUpdateContext);
};

export const ServerFilterProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [filterValue, setFilterValue] = useState<ServerFilter>(initialServerFilterValues);
  const setServerStatusFilter = (serverStatus: ServerStatus | undefined) => {
    setFilterValue({ ...filterValue, serverStatus });
  };
  const setPlayerFilter = (hasPlayers: boolean) => {
    setFilterValue({ ...filterValue, hasPlayers });
  };
  const setMapFilter = (mapNames: string[]) => {
    setFilterValue({ ...filterValue, mapNames });
  };

  return (
    <ServerFilterContext.Provider value={filterValue}>
      <ServerFilterUpdateContext.Provider
        value={{
          setMapFilter,
          setPlayerFilter,
          setServerStatusFilter,
          setFilter: setFilterValue,
          resetFilter: () => setFilterValue({ ...initialServerFilterValues }),
        }}
      >
        {children}
      </ServerFilterUpdateContext.Provider>
    </ServerFilterContext.Provider>
  );
};
