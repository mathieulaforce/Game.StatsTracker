import React, { useState } from 'react';
import { SearchIcon } from '@heroicons/react/solid';
import { GameServer } from '../../../types/gameServer';
import OnlineStateIndicator from '../../../components/ui/molecules/onlineStateIndicator';
import PasswordStateIndicator from '../../../components/ui/molecules/passwordStateIndicator';

interface ServerBrowserListBodyProps {
  gameServers: GameServer[];
  onServerClicked?: (ip: string) => void;
}

const ServerBrowserListBody: React.FC<ServerBrowserListBodyProps> = ({ gameServers, onServerClicked }) => {
  return (
    <tbody>
      {gameServers
        .sort((server1, server2) => server2.numberOfPlayers - server1.numberOfPlayers)
        .map((server) => (
          <tr
            key={server.ip}
            className="cursor-pointer odd:bg-slate-100 hover:bg-slate-500/30"
            onClick={() => {
              if (onServerClicked) return onServerClicked(server.ip);
            }}
          >
            <td className="text-center p-2">
              <OnlineStateIndicator isOnline={server.isOnline} iconClassName="w-5" />
              <PasswordStateIndicator isLocked={server.passwordProtected} iconClassName="w-5 text-black/50" />
            </td>
            <td className="p-2">{server.name}</td>
            <td className="p-2">{server.ip}</td>
            <td className="p-2">{server.mapName}</td>
            <td className="p-2">
              {server.numberOfPlayers} / {server.maxPlayers}
            </td>
            <td className="p-2">{server.version}</td>
          </tr>
        ))}
    </tbody>
  );
};
export default ServerBrowserListBody;
