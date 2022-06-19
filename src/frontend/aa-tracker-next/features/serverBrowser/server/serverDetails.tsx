import { useRouter } from 'next/router';
import { useEffect } from 'react';
import useDetailGameServer from '../../../hooks/gameServer/useDetailGameServer';
interface ServerDetailsProps {
  ip: string;
}
const ServerDetails: React.FC<ServerDetailsProps> = ({ ip }) => {
  const [isLoading, server] = useDetailGameServer(ip);

  if (isLoading) {
    return <div>loading server</div>;
  }
  if (!server) {
    return <div>Server not found</div>;
  }
  return (
    <div className="">
      <div>{server.server.hostname}</div>
      <div>{server.server.ipAddress}</div>
      <div>
        {server.matchInformation.currentRound} / {server.matchInformation.totalRounds}
      </div>
      <table>
        <tr>
          <td>name</td>
          <td>enemy</td>
          <td>kia</td>
          <td>frag rate</td>
          <td>goal</td>
          <td>leader</td>
          <td>roe</td>
        </tr>
        {server.scoreBoard.onlinePlayers.map((player) => (
          <tr key={player.name}>
            <td>{player.name}</td>
            <td>{player.enemy}</td>
            <td>{player.kia}</td>
            <td> {(player.enemy / (player.kia == 0 ? -10 : player.kia)) * -1}</td>
            <td>{player.goal}</td>
            <td>{player.leader}</td>
            <td>{player.roe}</td>
          </tr>
        ))}
      </table>
    </div>
  );
};

export default ServerDetails;
