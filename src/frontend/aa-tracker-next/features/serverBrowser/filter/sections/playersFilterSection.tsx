import React from 'react';
import LabelCheckbox from '../../../../components/ui/molecules/labelCheckbox';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';

interface PlayersFilterSectionProps {}

const PlayersFilterSection: React.FC<PlayersFilterSectionProps> = (props) => {
  const { hasPlayers } = useServerFilter();
  const { setPlayerFilter } = useUpdateServerFilter();
  return (
    <section>
      <header>Players</header>
      <div className="pl-2">
        <LabelCheckbox
          id="hasPlayers"
          label="Has players"
          onChange={() => {
            setPlayerFilter(!hasPlayers);
          }}
          isChecked={hasPlayers}
        />
      </div>
    </section>
  );
};

export default PlayersFilterSection;
