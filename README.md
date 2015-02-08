# WPFPunchCard
#### A WPF Punch Card control

### Usage:
```cs
<Window x:Class="WPFPunchCard.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:wpfPunchCard="clr-namespace:WPFPunchCard"
        Title="WPFPunchCard Demo" Height="350" Width="525" DataContext="{Binding Mode=OneWay, RelativeSource={RelativeSource Self}}">
    <DockPanel>
        <wpfPunchCard:PunchCard Data="{Binding Data}" />
    </DockPanel>
</Window>
```

Shape your data like this, and bind it to the 'Data' DependencyProperty.

```cs
Data = new List<Tuple<string, List<int>>>
{
    new Tuple<string, List<int>>("1", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
    new Tuple<string, List<int>>("2", new List<int> {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1}),
    new Tuple<string, List<int>>("3", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
    new Tuple<string, List<int>>("4", new List<int> {1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
    new Tuple<string, List<int>>("5", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 4, 1, 1, 1, 1, 1}),
    new Tuple<string, List<int>>("6", new List<int> {1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
    new Tuple<string, List<int>>("7", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1})
};
```
The string is the category label.
