import Panel from '../../../components/ui/atoms/panel';
import useGameSession from '../../../hooks/gameServer/useDetailGameServer';
import Image from 'next/image';
import { mapNameToImageSrc } from '../../../utils/serverMapUtilities';
import OnlineStateIndicator from '../../../components/ui/molecules/onlineStateIndicator';
import PasswordStateIndicator from '../../../components/ui/molecules/passwordStateIndicator';
import Spinner from '../../../components/ui/atoms/loaders/spinner';
import { CheckCircleIcon, XCircleIcon } from '@heroicons/react/solid';
import CopyToClipboardText from '../../../components/ui/atoms/copyToClipboardText';
import { Fragment } from 'react';

interface ServerDetailsProps {
  ip: string;
}
const ServerDetails: React.FC<ServerDetailsProps> = ({ ip }) => {
  const [isLoading, gameSession] = useGameSession(ip);

  if (isLoading) {
    return (
      <Spinner />
    );
  }
  if (!gameSession) {
    return <div>Server not found</div>;
  }
  return (
    <div className="text-slate-300 flex gap-2">
      <div>
        <Panel className="ring-1 ring-sky-500 ring-inset bg-opacity-50 ring-opacity-50 w-fit p-0">
          <div className="p-0">
            <div className="text-center bg-sky-700/50 rounded-t p-4 m-0">
              <h3> {gameSession.matchInformation.mapName}</h3>
            </div>
            <div className="  ">
              <img src={mapNameToImageSrc(gameSession.matchInformation.mapName, '300px')} alt={gameSession.matchInformation.mapName} className="w-full px-0.5" />
            </div>
            <div className="grid grid-cols-[auto_auto_auto_auto] h-max gap-2 p-4 ">
              <label className="capitalize text-slate-200">name:</label>
              <div className='col-span-3  text-right'>
                <CopyToClipboardText text={gameSession.server.hostname} />
              </div>
              <label className="capitalize">ip:</label>
              <div className='col-span-3  text-right'>
                <CopyToClipboardText text={gameSession.server.ipAddress} />
              </div>
              <label className="capitalize">admin name:</label>
              <div className='col-span-3  text-right'>
                <CopyToClipboardText text={gameSession.server.adminName} />
              </div>
              <label className="capitalize">Online:</label>
              <div className='mr-2'>
                <div>yes <OnlineStateIndicator isOnline iconClassName='w-5' /></div>
              </div>
              <label className="capitalize">Password:</label>
              <div>{gameSession.server.passwordProtected ? 'yes' : 'no'} <PasswordStateIndicator iconClassName='w-5 text-slate-500' isLocked={gameSession.server.passwordProtected} /></div>

              <label className="capitalize">players:</label>
              <div> {gameSession.scoreBoard.onlinePlayers.length}</div>

              <label className="capitalize">rounds:</label>
              <div>
                {gameSession.matchInformation.currentRound} /{gameSession.matchInformation.totalRounds}{' '}
              </div>

              <label className="capitalize"> time left:</label>
              <div> {gameSession.matchInformation.timeLeft}</div>

              <label className="capitalize">server version:</label>
              <div>{gameSession.server.version}</div>

              <label className="capitalize">cheats:</label>
              <div>{gameSession.server.cheats ? 'yes' : 'no'}</div>

              <label className="capitalize">miles:</label>
              <div>{gameSession.server.miles ? 'yes' : 'no'}</div>


            </div>
          </div>
        </Panel>
      </div>
      {!gameSession.scoreBoard.onlinePlayers.length && (
        <Panel className="ring-1 ring-sky-500 ring-inset bg-opacity-50 ring-opacity-50 w-fit p-0 flex-grow">
          <div className="p-0 h-full flex flex-col">
            <div className="text-center bg-sky-700/50 rounded-t p-4 m-0">
              <h3>Live Scoreboard</h3>
            </div>
            <div className="grid content-center justify-center flex-grow">
              <span className="text-lg animate-pulse   ">No players on the server</span>
            </div>
          </div>
        </Panel>
      )}
      {gameSession.scoreBoard.onlinePlayers.length > 0 && (
        <Panel className="ring-1 ring-sky-500 ring-inset bg-opacity-50 ring-opacity-50 w-fit p-0 flex-grow">
          <div className="p-0">
            <div className="text-center bg-sky-700/50 rounded-t p-4 m-0">
              <h3>Live Scoreboard</h3>
            </div>
            <div className="p-4">
              <div className="grid grid-cols-7 gap-4  justify-center">
                <div className="border-b border-sky-700 col-span-7"></div>
                <div className="capitalize">name</div>
                <div className="capitalize">enemy</div>
                <div className="capitalize">kia</div>
                <div className="capitalize">frag rate</div>
                <div className="capitalize">goal</div>
                <div className="capitalize">leader</div>
                <div className="capitalize">roe</div>
                <div className="border-b border-sky-700 col-span-7"></div>
                {gameSession.scoreBoard.onlinePlayers.map((player) => (
                  <Fragment key={player.name}>
                    <div>{player.name}</div>
                    <div>{player.enemy}</div>
                    <div>{player.kia}</div>
                    <div> {(player.enemy / (player.kia == 0 ? -10 : player.kia)) * -1}</div>
                    <div>{player.goal}</div>
                    <div>{player.leader}</div>
                    <div>{player.roe}</div>
                  </Fragment>
                ))}
              </div>
            </div>
          </div>
        </Panel>
      )}
    </div>
  );
};

export default ServerDetails;
