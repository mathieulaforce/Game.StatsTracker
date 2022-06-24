import * as React from 'react';
import { DuplicateIcon } from '@heroicons/react/solid';
import ToolTip from './toolTip/toolTip';

interface CopyToClipboardTextProps {
  text: string;
  className?: string;
}

const CopyToClipboardText: React.FC<CopyToClipboardTextProps> = ({ text, className }) => {
  return (
    <>
      {text}
      <DuplicateIcon className={`ml-2 w-4 cursor-pointer hover:text-sky-600 ${className}`} onClick={() => navigator.clipboard.writeText(text)} />
    </>
  );
};
export default CopyToClipboardText;
