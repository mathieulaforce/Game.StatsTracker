import React, { useContext, useEffect } from 'react';
import OnlineStateIcon from '../../../../components/ui/atoms/onlineStateIcon';
import LabelCheckbox from '../../../../components/ui/molecules/labelCheckbox';
import { ServerStatus } from '../../../../types/serverStatus';
import { serverStatusToBool } from '../../../../utils/serverStateUtils';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';

interface ServerStatusFilterSectionProps {}

const getNextServerStatus = (serverStatus: ServerStatus | undefined): ServerStatus | undefined => {
  if (serverStatus === 'online') {
    return 'offline';
  }
  if (serverStatus === 'offline') {
    return undefined;
  }
  return 'online';
};

const ServerStatusFilterSection: React.FC<ServerStatusFilterSectionProps> = (props) => {
  const { serverStatus } = useServerFilter();
  const { setServerStatusFilter } = useUpdateServerFilter();

  return (
    <section>
      <div className="flex gap-2 hover:text-sky-400 cursor-pointer" onClick={() => setServerStatusFilter(getNextServerStatus(serverStatus))}>
        <header>Server status</header>
        <OnlineStateIcon allowTriState={true} isOnline={serverStatusToBool(serverStatus)} className="block w-5 cursor-pointer hover:animate-spin" />
      </div>
    </section>
  );
};

export default ServerStatusFilterSection;
