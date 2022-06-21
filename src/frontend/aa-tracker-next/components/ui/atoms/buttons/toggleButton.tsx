import React from 'react';
interface ToggleButtonProps {
  isChecked: boolean;
  onChange?: (isChecked: boolean) => void;
  onClick?: (isChecked: boolean) => void;
}

export const ToggleButton: React.FC<ToggleButtonProps> = (props) => {
  return (
    <label htmlFor="checked-toggle" className="inline-flex relative items-center cursor-pointer" onClick={() => props.onClick?.(!props.isChecked)}>
      <input type="checkbox" value="" id="checked-toggle" className="sr-only peer" checked={props.isChecked} onChange={() => props.onChange?.(!props.isChecked)} />
      <div
        className={`w-11 h-6 rounded-full bg-slate-700
         peer peer-focus:ring-0 peer-checked:after:translate-x-full peer-checked:after:border-slate-300 after:content-[''] after:absolute after:top-0.5 after:left-[2px] after:bg-slate-300 after:border-slate-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all border-slate-600 peer-checked:bg-green-600`}
      ></div>
    </label>
  );
};

export default ToggleButton;
