import React from 'react';

interface LabelCheckboxProps {
  isChecked?: boolean;
  onChange: (isChecked: boolean) => void;
  label: string;
  id: string;
  className?: string;
}

const LabelCheckbox: React.FC<LabelCheckboxProps> = (props) => {
  return (
    <div className={props.className}>
      <div className="flex gap-2 items-center">
        <input
          type={'checkbox'}
          id={`lama-label-checkbox-${props.id}`}
          checked={props.isChecked}
          onChange={() => {
            props.onChange(!props.isChecked);
          }}
        />
        <label className="cursor-pointer select-none" htmlFor={`lama-label-checkbox-${props.id}`}>
          {props.label}
        </label>
      </div>
    </div>
  );
};

export default LabelCheckbox;
