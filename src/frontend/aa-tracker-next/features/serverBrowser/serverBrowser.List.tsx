import React from 'react';
import { GameServer } from '../../types/gameServer';
import OnlineStateIcon from '../../components/ui/atoms/onlineStateIcon';

interface ServerBrowserListProps {
  gameServers: GameServer[];
  onServerClicked?: (ip: string) => void;
}

const ServerBrowserList: React.FC<ServerBrowserListProps> = ({ gameServers, onServerClicked }) => {
  return (
    <table className="">
      <thead className="bg-slate-500 text-white">
        <tr>
          <th className=""></th>
          <th className="text-left p-2 capitalize">name</th>
          <th className="text-left p-2 capitalize">map</th>
          <th className="text-left p-2 capitalize">players</th>
          <th className="text-left p-2 capitalize">version</th>
        </tr>
      </thead>
      <tbody>
        {gameServers
          .sort((server1, server2) => server2.numberOfPlayers - server1.numberOfPlayers)
          .map((server) => (
            <tr
              key={server.ip}
              className="cursor-pointer odd:bg-slate-100"
              onClick={() => {
                if (onServerClicked) return onServerClicked(server.ip);
              }}
            >
              <td className="text-center p-2">
                <OnlineStateIcon isOnline={server.isOnline} className="w-5" />
              </td>
              <td className="p-2">{server.name}</td>
              <td className="p-2">{server.mapname}</td>
              <td className="p-2">
                {server.numberOfPlayers} / {server.maxPlayers}
              </td>
              <td className="p-2">{server.version}</td>
            </tr>
          ))}
      </tbody>
    </table>
  );
};
export default ServerBrowserList;
