// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import { Feature, FeatureType, FeatureChoicesType } from '../../models';
import { Text, Dropdown, PrimaryButton, DefaultButton, IDropdownOption } from '@fluentui/react';
import { useState } from "preact/hooks";

type Props = {
    feature: Feature,
    value: FeatureChoicesType,
    choices: FeatureType[],
    handleFeatureChange: (feature: Feature, newValue: FeatureType) => void
};

const FeatureCombobox: FunctionalComponent<Props> = (props) => {
    const {
        feature,
        value,
        choices,
        handleFeatureChange
    } = props;

    const [newValue, setNewValue] = useState(value);

    const hasChanged = value !== newValue;

    const canSave = hasChanged;
    const canCancel = hasChanged;

    const onChange = (event: React.FormEvent<HTMLDivElement>, item: IDropdownOption): void => {
        setNewValue(item.key);
    };

    const onValidateButtonClicked = () => {
        handleFeatureChange(feature, newValue);
    }

    const onCancelButtonClicked = () => {
        setNewValue(value);
    }

    const options: IDropdownOption[] = choices
        .map(c => {
            return {
                key: c,
                text: typeof c === 'string' ? c : c.toString()
            } as IDropdownOption;
        });

    return (
        <div>
            <p>
                <Text block variant="large">
                    {feature.name}
                </Text>
                {feature.description &&
                    <Text block variant="small">
                        {feature.description}
                    </Text>
                }
            </p>
            <Dropdown
                placeholder="Select an option"
                defaultSelectedKey={newValue}
                options={options}
                onChange={onChange}
            />
            <p>
                {canSave && (
                    <PrimaryButton
                        text="Save"
                        onClick={() => onValidateButtonClicked()}
                        allowDisabledFocus
                    />
                )}
                {canCancel && (
                    <DefaultButton
                        text="Cancel"
                        onClick={() => onCancelButtonClicked()}
                        allowDisabledFocus
                        style={{ marginLeft: 12 }}
                    />
                )}
            </p>
        </div>
    );
};

export default FeatureCombobox;
