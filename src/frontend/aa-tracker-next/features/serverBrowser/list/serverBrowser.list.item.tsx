import React from 'react';
import { GameServer } from '../../../types/gameServer';
import OnlineStateIndicator from '../../../components/ui/molecules/onlineStateIndicator';
import PasswordStateIndicator from '../../../components/ui/molecules/passwordStateIndicator';
import Panel from '../../../components/ui/atoms/panel';
import Avatar from '../../../components/ui/atoms/avatar';
import { mapNameToImageSrc } from '../../../utils/serverMapUtilities';
import { IconButton } from '../../../components/ui/atoms/buttons/iconButton';
import { SearchIcon } from '@heroicons/react/solid';
import ToolTip from '../../../components/ui/atoms/toolTip/toolTip';

interface ServerBrowserListItemProps {
  gameServer: GameServer;
  onServerClicked?: (ip: string) => void;
}

const ServerBrowserListItem: React.FC<ServerBrowserListItemProps> = ({ gameServer, onServerClicked }) => {
  return (
    <div className="flex flex-wrap">
      <Panel key={gameServer.ip} className="pl-2 w-[400px] bg-opacity-50 cursor-pointer hover:bg-sky-600 hover:ring-sky-600 hover:bg-opacity-10 transition-all duration-200">
        <div className="">
          <div className="flex justify-between">
            <div className="">
              <OnlineStateIndicator isOnline={gameServer.isOnline} iconClassName="w-5 mr-1 align-top" />
              <PasswordStateIndicator isLocked={gameServer.passwordProtected} iconClassName="w-5 text-slate-500 mx-1 align-top" />
              <div className="text-sky-400 inline-block cursor-pointer" onClick={() => onServerClicked?.(gameServer.ip)}>
                {gameServer.ip}
              </div>
            </div>
            <div>
              <ToolTip message="view details">
                <IconButton onClick={() => onServerClicked?.(gameServer.ip)} className="align-top" variant="primary" icon={<SearchIcon className="w-5" />}></IconButton>
              </ToolTip>
            </div>
          </div>
          <div>{gameServer.name}</div>
          <div className="flex items-center gap-2 mt-2 ">
            <Avatar imageSrc={mapNameToImageSrc(gameServer.mapName, '300px')} variant="rounded-square" size="medium" />
            <div>
              <div>{gameServer.mapName}</div>
              <div>
                Players: {gameServer.numberOfPlayers}/{gameServer.maxPlayers}
              </div>
            </div>
          </div>
        </div>
      </Panel>
    </div>
  );
};
export default ServerBrowserListItem;
