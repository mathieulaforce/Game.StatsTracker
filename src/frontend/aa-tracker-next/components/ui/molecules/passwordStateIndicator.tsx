import * as React from 'react';
import ToolTip from '../atoms/toolTip/toolTip';
import OnlineStateIcon from '../atoms/onlineStateIcon';
import { Position } from '../types/position';
import LockedStateIcon from '../atoms/lockedStateIcon';

interface PasswordStateIndicatorProps {
  isLocked?: boolean;
  iconClassName?: string;
  position?: Position;
}

const PasswordStateIndicator: React.FC<PasswordStateIndicatorProps> = ({ isLocked, iconClassName, position = 'top' }) => {
  return (
    <ToolTip message={isLocked ? 'Private' : 'Public'} position={position}>
      <LockedStateIcon isLocked={isLocked} className={iconClassName} />
    </ToolTip>
  );
};
export default PasswordStateIndicator;
