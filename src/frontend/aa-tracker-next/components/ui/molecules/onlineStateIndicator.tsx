import * as React from 'react';
import ToolTip from '../atoms/toolTip/toolTip';
import OnlineStateIcon from '../atoms/onlineStateIcon';
import { Position } from '../types/position';

interface OnlineStateIndicatorProps {
  isOnline?: boolean;
  iconClassName?: string;
  position?: Position;
}

const OnlineStateIndicator: React.FC<OnlineStateIndicatorProps> = ({ isOnline, iconClassName, position = 'top' }) => {
  return (
    <ToolTip message={isOnline ? 'Online' : 'Offline'} position={position}>
      <OnlineStateIcon isOnline={isOnline} className={iconClassName} />
    </ToolTip>
  );
};
export default OnlineStateIndicator;
