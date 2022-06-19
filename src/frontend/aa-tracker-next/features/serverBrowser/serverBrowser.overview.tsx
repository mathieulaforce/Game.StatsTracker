import { useEffect, useState } from 'react';
import ServerBrowserList from './list/serverBrowser.list';
import useGameServerList from '../../hooks/gameServer/useGameServerList';
import { GameServer } from '../../types/gameServer';
import ServerBrowserFilter from './filter/serverBrowser.filter';
import { useServerFilter } from './filter/ServerFilter';
import { useRouter } from 'next/router';

const ServerBrowserOverview: React.FC = () => {
  const [isLoading, servers] = useGameServerList();
  const router = useRouter();

  const [mapNames, setMapNames] = useState<string[]>([]);

  const [sortedServers, setSortedServer] = useState<GameServer[]>([]);
  const filter = useServerFilter();

  useEffect(() => {
    if (servers && servers.length) {
      const filteredServers = servers.filter((server) => {
        if (filter.hasPlayers && server.numberOfPlayers === 0) {
          return false;
        }
        if (filter.serverStatus === 'online' && !server.isOnline) {
          return false;
        }
        if (filter.mapNames.length && !~filter.mapNames.indexOf(server.mapName)) {
          return false;
        }
        return true;
        // ~server.name.toLowerCase().indexOf(filter.name.toLowerCase()) && ~server.mapName.toLowerCase().indexOf(filter.mapName.toLowerCase());
      });
      setSortedServer(
        filteredServers.sort((a, b) => {
          if (a.mapName === b.mapName) return 0;
          if (a.mapName < b.mapName) return -1;
          return 1;
        })
      );
    }
  }, [servers, filter]);

  if (isLoading) {
    return <div>loading servers</div>;
  }
  return (
    <div className="flex gap-2">
      <div>
        <ServerBrowserFilter mapNames={mapNames} />
      </div>
      <div className="flex-grow">
        <ServerBrowserList gameServers={sortedServers} onServerClicked={(ip) => router.push('/serverBrowser/' + ip)} />
      </div>
    </div>
  );
};

export default ServerBrowserOverview;
