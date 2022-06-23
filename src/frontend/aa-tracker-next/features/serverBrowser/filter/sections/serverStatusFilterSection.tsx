import React, { useContext, useEffect } from 'react';
import OnlineStateIcon from '../../../../components/ui/atoms/onlineStateIcon';
import ToolTip from '../../../../components/ui/atoms/toolTip/toolTip';
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
      <div className="flex gap-4 hover:text-sky-400 cursor-pointer" onClick={() => setServerStatusFilter(getNextServerStatus(serverStatus))}>
        <header>Server status</header>
        <ToolTip message={serverStatus ?? 'online / offline'}>
          <OnlineStateIcon allowTriState={true} isOnline={serverStatusToBool(serverStatus)} className="block w-5 cursor-pointer" />
        </ToolTip>
      </div>
    </section>
  );
};

export default ServerStatusFilterSection;
