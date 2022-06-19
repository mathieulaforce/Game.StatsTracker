import React, { useState } from 'react';
import { SearchIcon } from '@heroicons/react/solid';
import { GameServer } from '../../../types/gameServer';
import OnlineStateIndicator from '../../../components/ui/molecules/onlineStateIndicator';
import Avatar from '../../../components/ui/atoms/avatar';
import { mapNameToImageSrc } from '../../../utils/serverMapUtilities';
import ToolTip from '../../../components/ui/atoms/toolTip/toolTip';

interface ServerListItemProps {
  gameServer: GameServer;
  onServerClicked?: (ip: string) => void;
}

const ServerListItem: React.FC<ServerListItemProps> = ({ gameServer, onServerClicked }) => {
  return (
    <div className="rounded-lg bg-slate-400 flex ">
      <div className="flex items-center ml-1">
        <ToolTip message={gameServer.mapName}>
          <Avatar size="medium" variant="circle" imageSrc={mapNameToImageSrc(gameServer.mapName, '300px')} className="" />
        </ToolTip>
      </div>

      <div className="p-1 text-white w-full text-center">
        <h3 className="truncate">{gameServer.name}</h3>

        <div className="flex gap-2 justify-center">
          <div className="">{gameServer.ip}</div>
          <OnlineStateIndicator isOnline iconClassName="w-5" />
        </div>
        <div>{gameServer.mapName} </div>
      </div>
    </div>
  );
};
export default ServerListItem;
