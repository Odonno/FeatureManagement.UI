// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import { Feature, FeatureValueType } from '../../models';
import { Text, TextField, PrimaryButton, DefaultButton } from '@fluentui/react';
import { useState } from "preact/hooks";

type Props = {
    feature: Feature,
    value: number,
    handleFeatureChange: (feature: Feature, newValue: FeatureValueType) => void
};

const FeatureNumberInput: FunctionalComponent<Props> = (props) => {
    const {
        feature,
        value,
        handleFeatureChange
    } = props;

    const [newValue, setNewValue] = useState(value);
    
    const { readonly } = feature;

    const isNumeric = !isNaN(newValue);
    const hasChanged = value !== newValue;
    
    const canSave = hasChanged;
    const canCancel = hasChanged;

    const saveDisabled = !isNumeric;

    const onTextChanged = (e: React.KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const target = e.target as HTMLInputElement;
        setNewValue(parseFloat(target.value));
    }

    const onValidateButtonClicked = () => {
        handleFeatureChange(feature, newValue);
    }

    const onCancelButtonClicked = () => {
        setNewValue(value);
    }

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
            <TextField
                defaultValue={newValue.toString()}
                disabled={readonly}
                onKeyUp={onTextChanged}
            />
            {canSave && (
                <PrimaryButton 
                    text="Save" 
                    onClick={() => onValidateButtonClicked()} 
                    disabled={saveDisabled}
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
        </div>
    );
};

export default FeatureNumberInput;
