import React, { useContext } from 'react';
import LabelCheckbox from '../../../../components/ui/molecules/labelCheckbox';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';

interface ServerStatusFilterSectionProps {}

const ServerStatusFilterSection: React.FC<ServerStatusFilterSectionProps> = (props) => {
  const { serverStatus } = useServerFilter();
  const { setServerStatusFilter } = useUpdateServerFilter();
  return (
    <section>
      <header>Server status</header>
      <div className="pl-2">
        <LabelCheckbox
          id="online"
          label="Online"
          onChange={() => {
            if (serverStatus === undefined) {
              setServerStatusFilter('online');
            } else {
              setServerStatusFilter(undefined);
            }
          }}
          isChecked={serverStatus === 'online'}
        />
      </div>
    </section>
  );
};

export default ServerStatusFilterSection;
