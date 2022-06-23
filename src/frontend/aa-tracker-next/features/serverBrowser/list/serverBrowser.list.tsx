import React from 'react';
import Panel from '../../../components/ui/atoms/panel';
import { GameServer } from '../../../types/gameServer';
import ServerBrowserListItem from './serverBrowser.list.item';

interface ServerBrowserListProps {
  gameServers: GameServer[];
  onServerClicked?: (ip: string) => void;
}

const ServerBrowserList: React.FC<ServerBrowserListProps> = ({ gameServers, onServerClicked }) => {
  if (!gameServers?.length) {
    return (
      <div className="grid w-full h-full content-center justify-center ">
        <Panel className="text-lg animate-pulse rounded">No servers found</Panel>
      </div>
    );
  }
  return (
    <div className="flex flex-wrap gap-4 justify-center">
      {gameServers.map((server) => {
        return <ServerBrowserListItem key={server.ip} gameServer={server} onServerClicked={onServerClicked} />;
      })}
    </div>
  );
};
export default ServerBrowserList;
