import React from 'react';
import ToggleButton from '../../../../components/ui/atoms/buttons/toggleButton';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';

interface PlayersFilterSectionProps {}

const PlayersFilterSection: React.FC<PlayersFilterSectionProps> = (props) => {
  const { hasPlayers } = useServerFilter();
  const { setPlayerFilter } = useUpdateServerFilter();
  return (
    <section>
      <div className="flex gap-2 hover:text-sky-400 cursor-pointer">
        <header>Players</header>
        <ToggleButton
          isChecked={!!hasPlayers}
          onChange={() => {
            setPlayerFilter(!hasPlayers);
          }}
        />
      </div>
    </section>
  );
};

export default PlayersFilterSection;
