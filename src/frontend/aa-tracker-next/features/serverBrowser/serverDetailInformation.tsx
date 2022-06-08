import Image from 'next/image';
import * as React from 'react';
import { useMemo, useState } from 'react';
import useDetailGameServer from '../../hooks/gameServer/useDetailGameServer';
import { GameServer } from '../../types/gameServer';
import { mapNameToImageSrc } from '../../utils/serverMapUtilities';
interface ServerDetailInformationProps {
  ip: string | null;
}

const ServerDetailInformation: React.FC<ServerDetailInformationProps> = ({ ip }) => {
  const [isLoading, server] = useDetailGameServer(ip);
  if (!server) {
    return <>N/A</>;
  }
  if (isLoading) {
    return <>loading</>;
  }
  return (
    <div>
      <div>{server.matchInformation.mapName}</div>
      <Image
        src={mapNameToImageSrc(server.matchInformation.mapName, '300px')}
        alt="map"
        width="300px"
        height="100%"
        layout="responsive"
        objectFit="contain"
        objectPosition="right"
        className="text-right"
      />
      <div className="grid grid-cols-[max-content_auto]">
        <div className="px-2 font-bold">name:</div>
        <div className="px-2">{server.server.hostname}</div>
        <div className="px-2 font-bold">ip:</div>
        <div className="px-2">{server.server.ipAddress}</div>
        <div className="px-2 font-bold">round:</div>
        <div className="px-2">
          {server.matchInformation.currentRound} / {server.matchInformation.totalRounds}
        </div>
        <div className="px-2 font-bold">time left</div>
        <div className="px-2">
          <time>{server.matchInformation.timeLeft}</time>
        </div>
      </div>
      <div>
        <div>Score board</div>
        <table className="table-auto w-full">
          <thead>
            <tr>
              <td>Name</td>
              <td>Kills</td>
              <td>Kia</td>
              <td>K/D</td>
            </tr>
          </thead>
          <tbody>
            {server.scoreBoard.onlinePlayers.map((player) => {
              return (
                <tr key={player.name}>
                  <td>{player.name}</td>
                  <td>{player.enemy / 10}</td>
                  <td>{Math.abs(player.kia / 10)}</td>
                  <td>{player.kia == 0 ? (player.enemy / 10).toFixed(2) : (player.enemy / Math.abs(player.kia)).toFixed(2)}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};
export default ServerDetailInformation;
