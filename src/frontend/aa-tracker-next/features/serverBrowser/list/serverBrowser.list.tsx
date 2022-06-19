import React from 'react';
import { GameServer } from '../../../types/gameServer';
import ServerBrowserListHeader from './serverBrowser.list.header';
import ServerBrowserListBody from './serverBrowser.list.body';
import ServerListItem from './serverListItem';

interface ServerBrowserListProps {
  gameServers: GameServer[];
  onServerClicked?: (ip: string) => void;
}

const ServerBrowserList: React.FC<ServerBrowserListProps> = ({ gameServers, onServerClicked }) => {
  return (
    <table className="w-full">
      <ServerBrowserListHeader />
      <ServerBrowserListBody gameServers={gameServers} onServerClicked={onServerClicked} />
    </table>
  );
};
export default ServerBrowserList;
