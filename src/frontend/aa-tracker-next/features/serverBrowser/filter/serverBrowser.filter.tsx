import React from 'react';
import MapNamesFilterSection from './sections/mapNameFilterSection';
import PlayersFilterSection from './sections/playersFilterSection';
import ServerStatusFilterSection from './sections/serverStatusFilterSection';
import { useUpdateServerFilter } from './ServerFilter';
import { XIcon } from '@heroicons/react/solid';
import ToolTip from '../../../components/ui/atoms/toolTip/toolTip';
import Panel from '../../../components/ui/atoms/panel';

interface ServerBrowserFilterProps {
  mapNames: string[];
}

const ServerBrowserFilter: React.FC<ServerBrowserFilterProps> = (props) => {
  const { resetFilter } = useUpdateServerFilter();
  return (
    <Panel className="p-2 bg-opacity-50">
      <div className="flex gap-4 items-center">
        <ServerStatusFilterSection />
        <PlayersFilterSection />
        <MapNamesFilterSection mapNames={props.mapNames} />
        <ToolTip message="Reset filter">
          <span className="cursor-pointer text-red-500 w-5 " onClick={() => resetFilter()}>
            <XIcon className=" w-5 hover:rotate-12 transition-all" />
          </span>
        </ToolTip>
      </div>
    </Panel>
  );
};

export default ServerBrowserFilter;
