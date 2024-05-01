import React, { useState } from 'react';
import { clientGet } from '../utils/client';
import { PieChart, BarChart } from '@mui/x-charts';
import { Grid, Typography } from '@mui/material';
import { Tier } from '../objects/enums';
import { tierColors, tierNames } from '../objects/Item';

interface ComponentProps {

}

interface TierCount {
	tier: Tier;
	count: number;
}

interface CategoryCount {
	id: string;
	title: string;
	count: number;
}

interface DashboardResult {
	categoryCounts: Array<CategoryCount>;
	tierCounts: Array<TierCount>;
}

const Dashboard: React.FC<ComponentProps> = ({

}) => {
	const [dashboard, setDashboard] = useState<DashboardResult>({ categoryCounts: [], tierCounts: [] });

	React.useEffect(() => {
		clientGet('dashboard')
			.then((res) => {
				setDashboard(res);
			}).catch((message) => {
				
			});
	}, []);

	return <>
		<br />
		<Typography component="h4" variant="h4" color="primary" gutterBottom>
			Dashboard
		</Typography>
		<Grid container spacing={1}>
			<Grid item xs={6}>
				<Typography component="h6" variant="h6" color="default" gutterBottom>
					Category Items count
				</Typography>
				<PieChart
					series={[
						{
							data: dashboard.categoryCounts.map(q => {
								return {
									id: q.id,
									value: q.count,
									label: q.title,
								};
							}),
						},
					]}
					width={400}
					height={200}
				/>
			</Grid>
			<Grid item xs={6}>
				<Typography component="h6" variant="h6" color="default" gutterBottom>
					Tiers count
				</Typography>
				<PieChart
					series={[
						{
							data: dashboard.tierCounts.map((q, index) => {
								return {
									id: index,
									color: tierColors[q.tier],
									value: q.count,
									label: tierNames[q.tier],
								};
							}),
						},
					]}
					width={400}
					height={200}
				/>
			</Grid>
			<Grid item xs={6}>
				<Typography component="h6" variant="h6" color="default" gutterBottom>
					Category Items count
				</Typography>
				<BarChart
					series={[
						{ data: dashboard.categoryCounts.map(q => q.count) },
					]}
					height={200}
					width={400}
					xAxis={[{ data: dashboard.categoryCounts.map(q => q.title), scaleType: 'band' }]}
					margin={{ top: 10, bottom: 30, left: 40, right: 10 }}
				/>
			</Grid>
		</Grid>
	</>;
}

export default Dashboard;