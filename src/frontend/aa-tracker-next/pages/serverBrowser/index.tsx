import type { NextPage } from 'next';
import { useEffect, useState } from 'react';
import ServerBrowserList from '../../features/serverBrowser/serverBrowser.List';
import ServerDetailInformation from '../../features/serverBrowser/serverDetailInformation';
import useGameServerList from '../../hooks/gameServer/useGameServerList';

const Home: NextPage = () => {
  const [isLoading, servers] = useGameServerList();
  const [selectedServerIp, setSelectedServerIp] = useState<string | null>(null);
  useEffect(() => {
    if (servers.length) {
      setSelectedServerIp(servers[0].ip);
    }

    return () => {
      setSelectedServerIp(null);
    };
  }, [servers]);

  if (isLoading) {
    return <div>loading servers</div>;
  }
  return (
    <div className="grid grid-cols-[4fr_2fr] gap-4">
      <ServerBrowserList gameServers={servers} onServerClicked={setSelectedServerIp} />
      <ServerDetailInformation ip={selectedServerIp} />
    </div>
  );
};

export default Home;
